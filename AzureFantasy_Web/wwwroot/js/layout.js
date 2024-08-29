axios.get('/CheckLoginStatus', {
    withCredentials: true
})
    .then(response => {
        if (response.data.loggedIn === true) {
            //console.log('已登录')
            localStorage.setItem("loginStatus", "true");
        }
        if (response.data.loggedIn === false){
            //console.log('未登录')
            localStorage.setItem("avatar", "/images/avatar.jpg");
            localStorage.setItem("loginStatus", "false");
        }
    })
    .catch(error => {
        console.error('Error:', error)
    })



// 判断头像是否在localStorage中
if (localStorage.getItem("avatar")) {
    if (localStorage.getItem("loginStatus") === "true") {
        // 如果头像在localStorage中, 且登录状态是true, 则更新头像
        if (localStorage.getItem("avatar") === "/images/avatar.jpg" || localStorage.getItem("avatar") === "undefined") {
            console.log("avatar in localStorage is default but loginStatus is true")

            axios.get("/GetWebUsersInfo", {
                withCredentials: true
            })
                .then(response => {
                    localStorage.setItem("avatar", response.data.avatar)
                })
                .catch(error => {
                    console.error('Error:', error)
                })
        }
    }
    if (localStorage.getItem("loginStatus") === "false") {
        // 如果头像在localStorage中, 且登录状态是false, 则更新头像
        if (localStorage.getItem("avatar") !== "/images/avatar.jpg") {
            localStorage.setItem("avatar", "/images/avatar.jpg")
        }
    } 
} else if (localStorage.getItem("loginStatus") === "true") {
    console.log("avatar is not in localStorage but loginStatus is true")

    axios.get("/GetWebUsersInfo", {
        withCredentials: true
    })
        .then(response => {
            localStorage.setItem("avatar", response.data.avatar)
        })
        .catch(error => {
            console.error('Error:', error)
        })
} else if (localStorage.getItem("loginStatus") === "false") {
    localStorage.setItem("avatar", "/images/avatar.jpg")
}

const avatarImgSrc = localStorage.getItem("avatar")
// 设置头像链接
const titleAvatar = document.getElementById('title-avatar')
titleAvatar.setAttribute('src', avatarImgSrc)

// 获取ip
axios.get('https://api.ipify.org/?format=json')
    .then(response => {
        // 处理成功的响应（例如，重定向或显示成功消息）
        //console.log('你的IP地址是: ' + response.data.ip);
        localStorage.setItem("ip", response.data.ip);
    })

//console.log('主页JS加载正常')