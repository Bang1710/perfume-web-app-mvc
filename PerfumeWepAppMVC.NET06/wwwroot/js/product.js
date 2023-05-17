document.getElementById("form-filter-id").addEventListener("submit", function () {
    var gender = document.getElementsByName('gender');
    gender.forEach(g => {
        localStorage.setItem(g.id, g.checked);
    })

    var priceSortOrder = document.getElementsByName("priceSortOrder");
    priceSortOrder.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var brand = document.getElementsByName("brand");
    brand.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var capacity = document.getElementsByName("capacity");
    capacity.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    var original = document.getElementsByName("original");
    original.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })

    //var concentration = document.getElementsByName("concentration");
    //concentration.forEach(p => {
    //    localStorage.setItem(p.id, p.checked);
    //})
});

window.addEventListener("load", function () {
    var gender = document.getElementsByName('gender');
    var priceSortOrder = document.getElementsByName("priceSortOrder");
    var brand = document.getElementsByName("brand");
    var capacity = document.getElementsByName("capacity");
    var original = document.getElementsByName("original");
    //var concentration = document.getElementsByName("concentration");


    gender.forEach(g => {
        g.checked = localStorage.getItem(g.id) === "true";
    })
    priceSortOrder.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    brand.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    capacity.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    original.forEach(p => {
        p.checked = localStorage.getItem(p.id) === "true";
    })

    //concentration.forEach(p => {
    //    p.checked = localStorage.getItem(p.id) === "true";
    //})

});
