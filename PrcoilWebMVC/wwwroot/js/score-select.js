// 等待DOM准备就绪
document.addEventListener('DOMContentLoaded', function () {
    // 选择元素
    const searchBox = document.getElementById('search-box')
    const searchButton = document.getElementById('search-button')
    const searchInput = document.getElementById('search-input')
    const resultDiv = document.getElementById("result")
    
    window.search = function (recaptcha_token) {
        // 阻止默认的表单提交行为
        // event.preventDefault()

        // 获取用户名和密码的值
        const searchValue = searchInput.value;

        if (searchValue === "") {
            alert("请输入关键字")
            return false
        }

        // 显示加载动画
        searchButton.innerHTML = '加载中...';
        searchButton.disabled = true;
        
        const ip = localStorage.getItem("ip");
        // 使用Axios发送POST请求
        axios.get(`/GetStudentData/${searchValue}&${recaptcha_token}&${ip}`)
            .then(function (response) {
                // 处理成功的响应（例如，重定向或显示成功消息）
                // console.log(response.data)
                const data = response.data
                
                if (data.message === '验证不通过') {
                    alert('人机验证不通过! 请稍后再试, 或更换浏览器再次尝试')
                    // 隐藏加载动画
                    searchButton.innerHTML = '搜索';
                    searchButton.disabled = false;
                    return false
                }
                if (data.message === '验证服务器错误') {
                    alert('验证服务器错误!')
                    // 隐藏加载动画
                    searchButton.innerHTML = '搜索';
                    searchButton.disabled = false;
                    return false
                }
                if(data.message === '未找到该学生') {
                    alert('未找到该学生的数据!\n如果有重名,请在姓名后面加上班级\n例如: 张三1')
                    // 隐藏加载动画
                    searchButton.innerHTML = '搜索';
                    searchButton.disabled = false;
                    return false
                }
                
                const resultHtml = `
                <!--  -->
                <!-- <div style="background-image: url('${data.avatar}'); width: 200px; height: 218px; margin: 0 auto 20px;" class="main-cardBox-child">
                </div> -->
                
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>${data.class}班 ${data.name}</h2>
                <p>总分数: ${data.score}</p>
                <p>赋分总分数: ${data.assignscore}</p>
                <p>年级排名: ${data.graderanking}</p>
                <p>班级排名: ${data.classranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>语文</h2>
                <p>分数: ${data.chinese}</p>
                <p>年级排名: ${data.chineseranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>数学</h2>
                <p>分数: ${data.math}</p>
                <p>年级排名: ${data.mathranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>英语</h2>
                <p>分数: ${data.english}</p>
                <p>年级排名: ${data.englishranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>物理</h2>
                <p>分数: ${data.physics}</p>
                <p>年级排名: ${data.physicsranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>历史</h2>
                <p>分数: ${data.history}</p>
                <p>年级排名: ${data.historyranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>化学</h2>
                <p>分数: ${data.chemistry}</p>
                <p>分数(赋分): ${data.assignchemistry}</p>
                <p>年级排名: ${data.chemistryranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>生物</h2>
                <p>分数: ${data.organism}</p>
                <p>分数(赋分): ${data.assignorganism}</p>
                <p>年级排名: ${data.organismranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>政治</h2>
                <p>分数: ${data.politics}</p>
                <p>分数(赋分): ${data.assignpolitics}</p>
                <p>年级排名: ${data.politicsranking}</p>
                </div>
                <div style="background-color: #f8f9fa;" class="main-cardBox-child">
                <h2>地理</h2>
                <p>分数: ${data.geography}</p>
                <p>分数(赋分): ${data.assigngeography}</p>
                <p>年级排名: ${data.geographyranking}</p>
                </div>
                `;

                // 隐藏加载动画
                searchButton.innerHTML = '搜索';
                searchButton.disabled = false;

                searchBox.setAttribute('class', 'display-none')
                resultDiv.setAttribute('class', 'main-cardBox')

                resultDiv.innerHTML += resultHtml;
            })

            .catch(function (error) {
                // 处理错误（例如，显示错误消息）
                console.error(error);

                // 隐藏加载动画
                searchButton.innerHTML = '搜索';
                searchButton.disabled = false;
            });
    }
});