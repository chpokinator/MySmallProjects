var tokenKey = "accessToken"
const token = sessionStorage.getItem(tokenKey)
putValues()

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});

async function editBike(bikeId, title, price, wheelsize, info, color, material, file) {
    let response
    if (price == "") {
        price = 0
    }
    if (wheelsize == "") {
        wheelsize = 0
    }
    if (bikeId == 0) {
        response = await fetch('api/bikes', {

            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + token
            },
            body: JSON.stringify({
                bikeId: parseInt(bikeId),
                title,
                price: parseFloat(price),
                wheelSize: parseFloat(wheelsize),
                info,
                color,
                material,
                photo: file

            })


        })
    } else {


        response = await fetch('api/bikes', {

            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'bearer ' + token
            },
            body: JSON.stringify({
                bikeId: parseInt(bikeId),
                title,
                price: parseFloat(price),
                wheelSize: parseFloat(wheelsize),
                info,
                color,
                material,
                photo: file

            })


        })
    }

    if (response.ok === true) {
        alert("nice bro")
    } else {
        const errorData = await response.json()
        console.log(errorData)
        console.log(errorData.errors)
        if (errorData) {
            if (errorData.errors) {

                if (errorData.errors["Title"]) {
                    console.log(errorData["Title"])
                    showError(errorData.errors["Title"])
                }

                if (errorData.errors["Price"]) {
                    console.log(errorData["Price"])
                    showError(errorData.errors["Price"])
                }

                if (errorData.errors["WheelSize"]) {
                    console.log(errorData["WheelSize"])
                    showError(errorData.errors["WheelSize"])
                }

                if (errorData.errors["Color"]) {
                    console.log(errorData["Color"])
                    showError(errorData.errors["Color"])
                }

                if (errorData.errors["Material"]) {
                    console.log(errorData["Material"])
                    showError(errorData.errors["Material"])
                }

            } else {
                if (errorData["Title"]) {
                    showError(errorData["Title"])
                }

                if (errorData["Price"]) {
                    showError(errorData["Price"])
                    console.log(errorData["Price"])
                }

                if (errorData["WheelSize"]) {
                    showError(errorData["WheelSize"])
                }

                if (errorData["Color"]) {
                    showError(errorData["Color"])
                }

                if (errorData["Material"]) {
                    showError(errorData["Material"])
                }
            }




            document.getElementById('errors').style.display = 'block'
        }
    }
}


document.forms['editBikeForm'].addEventListener('submit', async function (e) {

    e.preventDefault()
    const form = document.forms['editBikeForm']
    const id = form.elements['bikeId'].value
    const title = form.elements['title'].value
    const price = form.elements['price'].value
    const wheelsize = form.elements['wheelsize'].value
    const info = form.elements['info'].value
    const color = form.elements['color'].value
    const material = form.elements['material'].value
    let file = null
    if (form.elements['file'].files.length > 0) {
        file = await toBase64(form.elements['file'].files[0])
    }
    console.log(file)
    editBike(id, title, price, wheelsize, info, color, material, file)

})


async function getBike(id) {

    let bike
    const response = await fetch('/api/bikes/' + id, {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })
    if (response.ok === true) {
        bike = await response.json()
    } else {
        bike = null
    }

    return bike
}

async function putValues() {
    var form = document.forms['editBikeForm']
    const urlParams = new URLSearchParams(window.location.search)
    var id = urlParams.get('id')

    var bike = await getBike(id)
    console.log(bike)
    form.elements['bikeId'].value = id
    form.elements['title'].value = bike.title
    form.elements['price'].value = bike.price
    form.elements['wheelsize'].value = bike.wheelSize
    form.elements['info'].value = bike.info
    form.elements['color'].value = bike.color
    form.elements['material'].value = bike.material
    form.elements['file'].value = bike.photo

}


function showError(errors) {
    errors.forEach(error => {
        const p = document.createElement('p')
        p.append(error)
        document.getElementById('errors').append(p)
    })
}

