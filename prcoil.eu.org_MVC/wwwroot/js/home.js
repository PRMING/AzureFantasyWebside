let resultDaily = document.getElementById("result");


function timestampToTime(timestamp) {
    timestamp = timestamp ? timestamp : null;
    let date = new Date(timestamp);//时间戳为10位需*1000，时间戳为13位的话不需乘1000
    let Y = date.getFullYear();
    let M = (date.getMonth() + 1 < 10 ? '0' + (date.getMonth() + 1) : date.getMonth() + 1)
    let D = (date.getDate() < 10 ? '0' + date.getDate() : date.getDate())
    let h = (date.getHours() < 10 ? '0' + date.getHours() : date.getHours())
    let m = (date.getMinutes() < 10 ? '0' + date.getMinutes() : date.getMinutes())
    let s = date.getSeconds() < 10 ? '0' + date.getSeconds() : date.getSeconds()
    return `${Y}年${M}月${D}日`;
}

// 创建一个数组来存储所有Axios promises
let axiosPromises = [];

for (i = 0; i < 10; i++) {
    // 获取时间戳
    let dateTime = Date.now()

    let aDay = 86400000

    // 时间戳减一天
    let dateTimeSubtractDay = dateTime - i * aDay

    searchDate = timestampToTime(dateTimeSubtractDay)

    // 将每个Axios请求的promise推到数组中
    axiosPromises.push(axios.get(`/DailyPageGet?showDate=${searchDate}`)
        .then(response => {
            const data = response.data;
            // 更新成绩信息
            const resultHtml = `
                    <div style="background-image: url(${data.imageUrl});" class="flexColumn main-cardBox-child">
                        <h2>${data.showTime}</h2>
                        <h2>${data.hitokoto}</h2>
                        <p> ——「${data.from}」</p>
                    </div>
                <!-- 在这里添加其他 -->
            `;
            return resultHtml;
        })
        .catch(error => {
            console.error("请求出错", error);
            resultDiv.innerHTML = "请求出错，请重试";
        })
    );
}

// 使用Promise.all等待所有promise解决
Promise.all(axiosPromises)
    .then(results => {
        // 按照正确的顺序更新HTML
        resultDaily.innerHTML = results.join('');
    });