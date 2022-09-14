import { useState } from "react";
import {
    Route,
    useHistory,
    Switch,
    Redirect,
    withRouter,
    useParams,
    NavLink,
    BrowserRouter
} from "react-router-dom";
import { useSessionStorage } from './useSessionStorage';
// import Blogs from './Blogs';
// import CreateBlog from './CreateBlog';
// import Register from './Register';
// import Subscriptions from './Subsciptions';



async function getTokenAsync(login, password) {
    const credentials = {
        login: login,
        password: password
    }

    const response = await fetch('https://localhost:44366/token', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
    })

    const data = await response.json()
    console.log(data)
    if (response.ok === true) {
        // sessionStorage.setItem("accessToken", data.access_token)
        sessionStorage.setItem("un", data.username)
        return data.access_token
    }
    console.log(response.status, data.errorText)
    return ""


}

async function getBikes() {


    const token = sessionStorage.getItem("accessToken")
    let bikes = [""]



    const response = await fetch('https://localhost:44316/api/bikes', {
        method: 'GET',
        headers: {
            'Authorization': 'bearer ' + token
        }
    })

    if (response.ok === true) {
        bikes = await response.json()
    }

    return bikes
}


function Main() {

    const [token, setToken] = useSessionStorage('token', '');
    const name = sessionStorage.getItem('un')

    if (token == '') {
        return (
            <BrowserRouter>
                <div>
                    <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                        <div className="container-fluid">
                            <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
                                <span className="navbar-toggler-icon"></span>
                            </button>
                            <div className="collapse navbar-collapse" id="navbarText">
                                <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                    <li className="nav-item">
                                        <NavLink className="nav-link" exact to="/" aria-current="page">Главная</NavLink>
                                    </li>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" to="/register" aria-current="page">Создать аккаунт</NavLink>
                                    </li>
                                </ul>
                                <span className="navbar-text">

                                    <NavLink className="btn btn-success" to="/login">Войти!</NavLink>

                                </span>
                            </div>
                        </div>
                    </nav>

                    <div className="d-flex justify-content-center">
                        <Switch>
                            {/* <Route exact path="/" component={Blogs}></Route>
                            <Route path="/register" component={Register}></Route> */}
                            <Route path="/login">
                                <LoginComp htoken={setToken} />
                            </Route>
                        </Switch>
                    </div>
                </div>

            </BrowserRouter>
        )
    }
    return (
        <BrowserRouter>
            <div>
                <nav className="navbar navbar-expand-lg navbar-dark bg-primary">
                    <div className="container-fluid">
                        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="collapse navbar-collapse" id="navbarText">
                            <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                                <li className="nav-item">
                                    <NavLink className="nav-link" exact to="/" aria-current="page">Главная</NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink className="nav-link" to="/register" aria-current="page">Создать аккаунт</NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink className="nav-link" to="/createblog" aria-current="page">Создать статью</NavLink>
                                </li>
                                <li className="nav-item">
                                    <NavLink className="nav-link" to="/subscriptions" aria-current="page">Мои подписки</NavLink>
                                </li>
                            </ul>
                            <span className="navbar-text">
                                <label className="me-3">{name}</label>
                                <button className="btn btn-success" onClick={() => {
                                    setToken("")
                                }}>Выйти</button>
                            </span>
                        </div>
                    </div>
                </nav>

                <div className="d-flex justify-content-center">

                    <Switch>
                        {/* <Route exact path="/" component={Blogs}></Route>
                        <Route path="/register" component={Register}></Route>
                        <Route path="/login">
                            <Login />
                        </Route> */}
                        <Route path="/login">
                            <LoginComp htoken={setToken} />
                        </Route>
                    </Switch>
                </div>
            </div>

        </BrowserRouter>
    )



}

function LoginComp(props) {

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')

    return (
        <div className="d-flex p-5 gap-3 flex-column" style={{width: '500px'}}>


            <input className="form-control" type='text' placeholder="Логин" onChange={(e) => (setUsername(e.target.value))}></input>
            <input className="form-control" type="password" placeholder="Пароль" onChange={(e) => (setPassword(e.target.value))}></input>
            <button className="btn btn-secondary" onClick={async () => {
                let tmp = await getTokenAsync(username, password)
                console.log(tmp)
                props.htoken(tmp)
            }}>Залогиниться</button>
            <label>{username}</label>
            <label>{password}</label>


        </div>
    )
}



export default Main