var tokenKey = "accessToken"

const toBase64 = file => new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = error => reject(error);
});


async function getBikes() {


    const token = sessionStorage.getItem(tokenKey)

    

    const response = await fetch('https://localhost:44316/api/bikes', {
        method: 'GET',
        headers: {
            'Authorization' : 'bearer ' + token
        }
    })

    if (response.ok === true) {
        const bikes = await response.json()
        let rows = document.querySelector('tbody')
        rows.innerHTML = ''
        bikes.forEach(bike => rows.append(createTableRow(bike)))
    }
}


function createTableRow(bike) {
    const tr = document.createElement('tr')
    const photoTd = document.createElement('td')
    const photo = document.createElement('img')
    photo.src = `${bike.photo}`
    photo.style.width = "20px"
    photo.style.height = "20px"
    photoTd.append(photo)
    tr.append(photoTd)
    const titleTd = document.createElement('td')
    titleTd.append(bike.title)
    tr.append(titleTd)
    const priceTd = document.createElement('td')
    priceTd.append(bike.price)
    tr.append(priceTd)
    const wheelsTd = document.createElement('td')
    wheelsTd.append(bike.wheelSize)
    tr.append(wheelsTd)
    const infoTd = document.createElement('td')
    infoTd.append(bike.info)
    tr.append(infoTd)
    const colorTd = document.createElement('td')
    colorTd.append(bike.color)
    tr.append(colorTd)
    const materialTd = document.createElement('td')
    materialTd.append(bike.material)
    tr.append(materialTd)
    const editTd = document.createElement('td')
    const editBtn = document.createElement('a')
    editBtn.href = `https://localhost:44316/editBike.html?id=${bike.bikeId}`
    editBtn.className += "btn btn-primary"
    editBtn.append('Edit')
    editTd.append(editBtn)
    tr.append(editTd)
    const deleteTd = document.createElement('td')
    const deleteBtn = document.createElement('button')
    deleteBtn.className += "btn btn-primary"
    deleteBtn.id = `id${bike.bikeId}`
    deleteBtn.append('Delete')
    deleteBtn.addEventListener('click', function (e) {
        e.preventDefault()
        console.log('pee pee poo')
        deleteBike(bike.bikeId)
    })
    deleteTd.append(deleteBtn)
    tr.append(deleteTd)


    return tr
}

async function createBike(title, price, wheelsize, info, color, material, file) {


    const token = sessionStorage.getItem(tokenKey)
    console.log(price)
    console.log(wheelsize)

    if (price === "") {
        price = 0
        console.log(price)
    }
    if (wheelsize === "") {
        wheelsize = 0
        console.log(wheelsize)
    }

    const response = await fetch('https://localhost:44316/api/bikes', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
        body: JSON.stringify({
            title,
            price: parseFloat(price),
            wheelSize: parseFloat(wheelsize),
            info,
            color,
            material,
            photo: file

        })
    })

    if (response.ok === true) {
        const bike = await response.json()
        document.querySelector('tbody').append(createTableRow(bike))
    } else {
        const errorData = await response.json()
        
        console.log(errorData)
        if (errorData) {
            if (errorData.errors) {

                if (errorData.errors["Title"]) {
                    showError(errorData.errors["Title"])
                }

                if (errorData.errors["Price"]) {
                    showError(errorData.errors["Price"])
                }

                if (errorData.errors["WheelSize"]) {
                    showError(errorData.errors["WheelSize"])
                }

                if (errorData.errors["Color"]) {
                    showError(errorData.errors["Color"])
                }

                if (errorData.errors["Material"]) {
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



async function deleteBike(id) {
    const token = sessionStorage.getItem(tokenKey)
    const response = await fetch('https://localhost:44316/api/bikes/' + id, {
        method: 'DELETE',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        getBikes()
    }
}


//document.forms['bikeForm'].addEventListener('submit', async function (e) {
//    e.preventDefault()
//    const form = document.forms['bikeForm']
//    const title = form.elements['title'].value
//    const price = form.elements['price'].value
//    const wheelsize = form.elements['wheelsize'].value
//    const info = form.elements['info'].value
//    const color = form.elements['color'].value
//    const material = form.elements['material'].value
//    const file = await toBase64(form.elements['file'].files[0])
//    createBike(title, price, wheelsize, info, color, material, file)

//})

function showError(errors) {
    errors.forEach(error => {
        const p = document.createElement('p')
        p.append(error)
        document.getElementById('errors').append(p)
    })
}

getBikes()

async function getTokenAsync() {
    const credentials = {
        login: document.querySelector('#login').value,
        password: document.querySelector('#password').value
    }

    const response = await fetch('https://localhost:44316/api/account/token', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()
    if (response.ok === true) {
        sessionStorage.setItem(tokenKey, data.access_token)
        getBikes()
    } else {
        console.log(response.status, data.errorText)
    }

}


document.getElementById('submitLogin').addEventListener('click', function (e) {
    console.log("nu blyat")
    getTokenAsync()
    alert('ff')
})