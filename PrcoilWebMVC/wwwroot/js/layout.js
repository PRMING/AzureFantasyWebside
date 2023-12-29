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


console.log('主页JS加载正常')