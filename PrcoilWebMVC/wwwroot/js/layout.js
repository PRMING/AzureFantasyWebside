window.globalUnlogin = '/NotLoggedIn'

if (localStorage.getItem("avatar")) {
    console.log("avatar is already in localStorage")
}
else {
    console.log("avatar is not in localStorage")
    localStorage.setItem("avatar", "/images/avatar.jpg");
}
const avatarImgSrc = localStorage.getItem("avatar")
// 设置头像链接
const titleAvatar = document.getElementById('title-avatar')
titleAvatar.setAttribute('src', avatarImgSrc)

// 获取ip
axios.get('https://api.ipify.org/?format=json')
    .then(function (response) {
        // 处理成功的响应（例如，重定向或显示成功消息）
        console.log(response.data.ip);
        localStorage.setItem("ip", response.data.ip);
    })

console.log('主页JS加载正常')