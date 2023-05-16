
document.getElementById("form-filter-id").addEventListener("submit", function () {
    var gender = document.getElementsByName('gender');
    gender.forEach(g => {
        localStorage.setItem(g.id, g.checked);
    })

    var priceSortOrder = document.getElementsByName("priceSortOrder");
    priceSortOrder.forEach(p => {
        localStorage.setItem(p.id, p.checked);
    })
});

window.addEventListener("load", function () {
    var gender = document.getElementsByName('gender');
    var priceSortOrder = document.getElementsByName("priceSortOrder");
    gender.forEach(g => {
        g.checked = this.localStorage.getItem(`${g.id}`) === "true";
    })
    priceSortOrder.forEach(p => {
        p.checked = this.localStorage.getItem(`${p.id}`) === "true";
    })
});
