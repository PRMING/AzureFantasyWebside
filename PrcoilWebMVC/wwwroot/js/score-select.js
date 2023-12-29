// 等待DOM准备就绪
document.addEventListener('DOMContentLoaded', function () {
    // 选择元素
    const searchBox = document.getElementById('search-box');
    const searchButton = document.getElementById('search-button');
    const searchInput = document.getElementById('search-input');
    const resultDiv = document.getElementById("result");

    // 为按钮添加事件监听器
    searchButton.addEventListener('click', function (event) {
        // 阻止默认的表单提交行为
        event.preventDefault();

        // 获取用户名和密码的值
        const searchValue = searchInput.value;

        if (searchValue === "") {
            alert("请输入关键字");
            return false
        }

        // 使用Axios发送POST请求
        axios.get(`/GetStudentData?Name=${searchValue}`)
            .then(function (response) {
                // 处理成功的响应（例如，重定向或显示成功消息）
                console.log(response.data);

                const data = response.data;

                const resultHtml = `
                <!--  -->
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

                searchBox.setAttribute('class', 'display-none')
                resultDiv.setAttribute('class', 'main-cardBox')

                resultDiv.innerHTML += resultHtml;
            })

            .catch(function (error) {
                // 处理错误（例如，显示错误消息）
                console.error(error);
            });
    });
});