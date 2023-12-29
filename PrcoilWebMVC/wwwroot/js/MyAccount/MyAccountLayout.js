// 点击按钮
document.addEventListener('DOMContentLoaded', function () {
    const loginOutButton = document.getElementById('loginOutButton')
    loginOutButton.addEventListener('click', function () {
        // 使用Axios发送POST请求
        axios.get('/UserLogout')
            .then(function (response) {
                if (response.data.redirectUrl) {
                    localStorage.removeItem("avatar");
                    window.location.href = response.data.redirectUrl;
                }
            })
            .catch(function (error) {
                // 处理错误的响应
                console.log(error);
            });
    })
})