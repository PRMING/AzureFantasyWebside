// 变量
const loginTabRegister = document.getElementById('tab-reg')
const loginTabLogin = document.getElementById('tab-login')
const loginBox = document.getElementById('login-box')
const registerBox = document.getElementById('reg-box')

// 注册按钮点击事件
loginTabRegister.onclick = function () {
    loginBox.setAttribute('class', 'main__login-reg-box displayNone')
    registerBox.setAttribute('class', 'main__login-reg-box')
}
loginTabLogin.onclick = function () {
    registerBox.setAttribute('class', 'reg-box displayNone');
    loginBox.setAttribute('class', 'login-box')
}

// 等待DOM准备就绪
document.addEventListener('DOMContentLoaded', function () {
    // 选择表单和按钮
    // const form = document.getElementById('login-box__login-form')
    const loginButton = document.getElementById('login-button')

    // 为按钮添加事件监听器
    loginButton.addEventListener('click', function (event) {
        // 阻止默认的表单提交行为
        event.preventDefault()

        // 获取用户名和密码的值
        const username = document.getElementById('login-username').value;
        const password = document.getElementById('login-password').value;

        if (password === "" || username === "") {
            alert("请正确输入账号密码!");
            return false
        }

        // 使用Axios发送POST请求
        axios.post('/UserLogin', {
            cellphone: username,
            password: password,
            captcha: "none",
            remember: "none"
        })
            .then(function (response) {
                // 处理成功的响应（例如，重定向或显示成功消息）
                console.log(response.data);

                if (response.status === 200) {
                    // 根据实际的响应头部字段名称获取令牌
                    const token = response.headers['authorization'];//必须小写
                    console.log(token);
                    // 保存令牌到 Cookie
                    document.cookie = `token=${token}; path=/`;

                    if (response.data.message === "已登录,Token已发送") {
                        // 重定向到登录成功后的页面
                        window.location.href = '/ScoreSelect'; // 跳转的URL
                    }
                    else if (response.data.message === "未找到账户") {
                        alert("未找到账户!");
                    }
                    else if (response.data.message === "密码错误") {
                        alert("密码错误!");
                    }
                }
                else {
                    console.log("HTTP ERROR");
                }
            })

            .catch(function (error) {
                // 处理错误（例如，显示错误消息）
                console.error(error);
            });
    })
})