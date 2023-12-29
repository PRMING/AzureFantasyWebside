// 变量
const loginTabRegister = document.getElementById('tab-reg')
const loginTabLogin = document.getElementById('tab-login')
const loginBox = document.getElementById('login-box')
const registerBox = document.getElementById('reg-box')
const errorBox = document.getElementById('error-box')

// 注册tab点击事件
loginTabRegister.onclick = function () {
    loginBox.classList.add('display-none');
    registerBox.classList.remove('display-none');
}
loginTabLogin.onclick = function () {
    registerBox.classList.add('display-none');
    loginBox.classList.remove('display-none');
}

// 等待DOM准备就绪
document.addEventListener('DOMContentLoaded', function () {
    // 选择表单和按钮
    // const form = document.getElementById('login-box__login-form')
    const loginButton = document.getElementById('login-button')
    const regButton = document.getElementById('reg-button')

    // 错误框显示
    function resultError(errorMessage) {
        // 错误html
        const errorHtml = `<p class="error-box__message">${errorMessage}</p>`
        // 显示错误box
        errorBox.classList.remove('display-none');
        // 插入错误信息
        errorBox.innerHTML = errorHtml

        // 移除动画类（如果存在），然后重新添加以触发动画
        errorBox.classList.remove('main__error-box__animation');
        void errorBox.offsetWidth; // 强制重绘
        errorBox.classList.add('main__error-box__animation');
    }

    // 为按钮添加事件监听器
    // 登录
    loginButton.addEventListener('click', function (event) {
        // 阻止默认的表单提交行为
        event.preventDefault()

        // 获取用户名和密码的值
        const username = document.getElementById('login-username').value;
        const password = document.getElementById('login-password').value;

        if (password === "" || username === "") {
            resultError("请输入正确的账号和密码")
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
                    if (response.data.redirectUrl) {
                        $.ajax({
                            url: '/GetWebUsersInfo',
                            type: 'GET',
                            async: false, // 将async设置为false以进行同步请求
                            success: function (data) {
                                // 处理响应
                                console.log(data.avatar);
                                localStorage.setItem("avatar", data.avatar);
                            },
                            error: function (error) {
                                console.error('Error:', error);
                            }
                        });
                        window.location.href = response.data.redirectUrl;
                    }
                    else if (response.data.message === "未找到账户") {
                        resultError("未找到账户")
                    }
                    else if (response.data.message === "密码错误") {
                        resultError("密码错误")
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

    // 注册
    window.register = function Register(recaptcha_token) {
        // 阻止默认的表单提交行为
        // event.preventDefault()

        // 获取用户名和密码的值
        const username = document.getElementById('reg-username').value
        const phone = document.getElementById('reg-phone').value
        const password = document.getElementById('reg-password').value
        const passwordAgain = document.getElementById('reg-password-again').value

        //检查密码强度函数 不正确输出false
        function checkPwd(pwd) {
            let reg = /^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$/
            let re = new RegExp(reg)
            return re.test(pwd)
        }
        function checkUsn(usn) {
            let reg = /^[0-9A-Za-z]{4,10}$/;
            let re = new RegExp(reg)
            return re.test(usn)
        }
        function checkPhone(usn) {
            let reg = /^[0-9]{11}$/;
            let re = new RegExp(reg)
            return re.test(usn)
        }
        
        

        // 检查手机号
        if (!checkUsn(username)) {
            resultError("用户名必须为4-6位数字或英文字母")
            return false
        }
        // 检查用户名
        if (!checkPhone(phone)) {
            resultError("请检查是否正确输入11位中国大陆手机号")
            return false
        }
        // 检查两次密码是否一致
        if (password !== passwordAgain) {
            resultError("两次输入的密码不一致")
            return false
        }
        // 检查密码组合
        if (!checkPwd(password)) {
            resultError("密码必须为6-16位数字与英文字母组合")
            return false
        }

        // 开始注册
        // 使用axios发送POST请求
        axios.post('/UserRegister', {
            Cellphone: phone,
            Username: username,
            Password: password,
            RecaptchaToken : recaptcha_token,
        })
            .then(response => {
                //弹出框
                console.log('成功发送JSON数据到后端')
                // 获取服务器响应
                const data = response.data
                console.log(data.message)

                // 判断服务器返回状态
                if (data.message === '已注册') {
                    // 显示数据
                    resultError('注册成功，请登录')
                }
                if (data.message === '账户已被注册') {
                    // 显示数据
                    resultError('手机号已经被注册了哦，如果忘记密码请找回')
                }
                if (data.message === '验证不通过') {
                    // 显示数据
                    resultError('reCaptcha 验证不通过，请确保操作合法')
                }
                if (data.message === '验证服务器错误') {
                    // 显示数据
                    resultError('reCaptcha 验证服务器错误!')
                }
            })
            .catch(error => {
                //弹出框
                alert('发送JSON数据到后端失败')
            })
    }
})