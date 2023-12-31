// 设置头像链接
const basicAvatar = document.getElementById('basicAvatar')
basicAvatar.setAttribute('src', avatarImgSrc)

const basicWelcome = document.getElementById('basicWelcome')

// 获取信息
axios.get('/GetWebUsersInfo')
    .then(function (response) {
        const data = response.data;
        // console.log(data);
        basicWelcome.innerText = `欢迎使用，${data.username}`
        
    })
    .catch(function (error) {
        // 处理错误的响应
        console.log(error);
    });