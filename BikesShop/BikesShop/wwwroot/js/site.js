// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#bikes-list').load(`/Home/BikesList`)

//function test() {
//    let selectColor = $("#color").val()
//    let selectMaterial = $("#material").val()
//    let options = [`${selectMaterial}`, `${selectColor}`]
//    return options
//}


$('#search').on('click', function () {
    let searchText = $('#searchText').val()

    $('#bikes-list')
        .load(`/Home/BikesList/?search=${searchText}`)
})

$('#byTitle').on('click', function () {
    let color = $('#color').val()
    let material = $('#material').val()
    $('#bikes-list').load(`/Home/BikesList/?color=${color}&material=${material}&sortType=tAsc`)
})

$('#byPrice').on('click', function () {
    let color = $('#color').val()
    let material = $('#material').val()
    $('#bikes-list').load(`/Home/BikesList/?color=${color}&material=${material}&sortType=pAsc`)
})

//$('#filter').on('click', function () {

//    $('#bikes-list').load(`/Home/BikesList/?material=${selectColor.text}&color=${selectMaterial.text}`)
//})


$('#filter').on('click', function () {
    let search = $('#searchText').val()
    let color = $('#color').val()
    let material = $('#material').val()
    let sort = $('#sort').val()
    
    $('#bikes-list').load(`/Home/BikesList/?color=${color}&material=${material}&search=${search}&sortType=${sort}`)

})

