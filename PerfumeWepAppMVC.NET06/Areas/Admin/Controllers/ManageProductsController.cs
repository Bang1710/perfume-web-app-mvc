using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeWebApp.NET06.Data;
using PerfumeWepAppMVC.NET06.Models;


namespace PerfumeWepAppMVC.NET06.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManageProductsController : Controller
    {
        private readonly PerfumeDBContext _context;

        public ManageProductsController(PerfumeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/ManageProducts
        [Route("quan-ly-san-pham")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8)
        {
            var perfumeDBContext = _context.Products.Include(p => p.Category)
                                            .OrderBy(p => p.Product_ID)
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(_context.Products.Count() / (double)pageSize);
            return View(perfumeDBContext);
        }

        // GET: Admin/ManageProducts/Details/5
        [Route("quan-ly-san-pham/chi-tiet-san-pham/{id?}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Product_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/ManageProducts/Create
        [Route("quan-ly-san-pham/tao-moi-san-pham")]
        public IActionResult Create()
        {
            ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name");
            return View();
        }

        // POST: Admin/ManageProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("quan-ly-san-pham/tao-moi-san-pham")]
        public async Task<IActionResult> Create([Bind("Product_ID,Category_ID,Product_Name,Product_Price,Product_Origin,Product_Gender,Product_Style,Product_ReleaseYear,Product_Volume,Product_IsNew,Product_IsRecommend,Product_IsTrending,Product_Intro")] Product product)
        {
            ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name", product.Category != null ? product.Category.Category_ID : null);
            if (_context.Products.Any(p => p.Product_ID == product.Product_ID))
            {
                ModelState.AddModelError("Product_ID", "Mã sản phẩm này đã tồn tại");
                return View(product);
            }

            if (product.Product_ReleaseYear <= 2000 || product.Product_ReleaseYear >= (int)DateTime.Now.Year)
            {
                ModelState.AddModelError("Product_ReleaseYear", $"Năm phát hành phải lớn hơn năm 200 và nhỏ hơn năm {DateTime.Now.Year}");
                return View(product);
            }

            if (product.Product_Gender != "Nam" || product.Product_Gender != "Nữ" || product.Product_Gender != "Unisex")
            {
                ModelState.AddModelError("Product_Gender", "Giá trị của giới tính bắt buộc là Nam hoặc Nữ hoặc Unisex");
                return View(product);
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/ManageProducts/Edit/5
        [Route("quan-ly-san-pham/chinh-sua-san-pham/{id?}")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            //ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name", product.Category.Category_Name);
            ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name", product.Category != null ? product.Category.Category_ID : null);
            return View(product);
        }

        // POST: Admin/ManageProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Product_ID,Category_ID,Product_Name,Product_Price,Product_Origin,Product_Gender,Product_Style,Product_ReleaseYear,Product_Volume,Product_IsNew,Product_IsRecommend,Product_IsTrending,Product_Intro")] Product product)
        {
            if (id != product.Product_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name", product.Category != null ? product.Category.Category_ID : null);
            //ViewData["Category_ID"] = new SelectList(_context.Categories, "Category_ID", "Category_Name", product.Category.Category_Name);
            return View(product);
        }

        // GET: Admin/ManageProducts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Product_ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/ManageProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'PerfumeDBContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
          return (_context.Products?.Any(e => e.Product_ID == id)).GetValueOrDefault();
        }
    }
}
