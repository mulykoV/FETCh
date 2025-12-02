// ==========================================
// 1. ГРА "GIT COMMIT" (З ПЕРЕВІРКОЮ)
// ==========================================
(() => {
    // Спочатку знаходимо головні елементи
    const commitBtn = document.getElementById("commitBtn");
    const resultBox = document.getElementById("resultBox");
    const scoreEl = document.getElementById("score");
    const timerEl = document.getElementById("timer");
    const typedText = document.getElementById("typedText");
    const consoleLine = document.getElementById("consoleLine");

    // === ГОЛОВНА ПЕРЕВІРКА ===
    // Якщо кнопки гри немає на сторінці, ми просто виходимо з цієї функції.
    // Це запобігає помилкам на сторінці Реєстрації та інших.
    if (!commitBtn || !resultBox) return;

    let score = 0;
    let timeLeft = 10;
    let gameActive = false;
    let countdown = null;

    function startGame() {
        if (gameActive) return;
        gameActive = true;
        score = 0;
        timeLeft = 10;
        scoreEl.textContent = score;
        timerEl.textContent = `Час: ${timeLeft}`;
        resultBox.classList.add("hidden");
        resultBox.innerHTML = "";
        consoleLine.style.display = "flex";
        commitBtn.style.display = "inline-flex";

        countdown = setInterval(() => {
            timeLeft--;
            timerEl.textContent = `Час: ${timeLeft}`;

            if (timeLeft <= 0) {
                clearInterval(countdown);
                gameActive = false;
                timerEl.textContent = "";
                showCertificate();
            }
        }, 1000);
    }

    function showCertificate() {
        // Отримуємо URL для реєстрації безпечно
        const urlElement = document.getElementById("registerUrl");
        const registerUrl = urlElement ? urlElement.dataset.url : "/";

        // Ховаємо рядок консолі
        if (consoleLine) consoleLine.style.display = "none";

        resultBox.innerHTML = `
            <div class="success-text">
              <h2>🎉 Вітаємо!</h2>
              <p>Ви зробили <b>${score}</b> коммітів за 10 секунд.</p>
              <p>Щоб рухатись далі — приєднайтесь до нас!</p>
              <a href="${registerUrl}" class="join-btn">Приєднатися до нас</a>
            </div>
        `;
        resultBox.classList.remove("hidden");
    }

    commitBtn.addEventListener("click", () => {
        if (!gameActive) startGame();
        if (gameActive && timeLeft > 0) {
            score++;
            scoreEl.textContent = score;
        }
    });

    function typeGitCommit() {
        if (!typedText) return;
        typedText.textContent = "";
        let index = 0;
        const text = "git commit";
        const speed = 120;
        (function typeEffect() {
            if (index < text.length) {
                typedText.textContent += text.charAt(index++);
                setTimeout(typeEffect, speed);
            }
        })();
    }

    // Ініціалізація при завантаженні сторінки
    window.addEventListener("DOMContentLoaded", () => {
        resultBox.classList.add("hidden");
        resultBox.innerHTML = "";
        if (consoleLine) consoleLine.style.display = "flex";
        commitBtn.style.display = "inline-flex";
        timerEl.textContent = "Час: 10";
        typeGitCommit();
    });
})();


// ==========================================
// 2. Scroll reveal (Анімація появи при скролі)
// ==========================================
const animatedBlocks = document.querySelectorAll(".scroll-animate");

// Перевірка: створюємо Observer тільки якщо є блоки для анімації
if (animatedBlocks.length > 0) {
    const observer = new IntersectionObserver(
        entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("visible");
                }
            });
        },
        { threshold: 0.2 }
    );

    animatedBlocks.forEach(block => observer.observe(block));
}


// ==========================================
// 3. Дизайн на сторінці Реєстрація (МАТРИЦЯ / ГЛІТЧ)
// ==========================================
const canvas = document.getElementById('matrixRain');

// ПЕРЕВІРКА: Код всередині виконається ТІЛЬКИ якщо canvas знайдено на сторінці
if (canvas) {
    const ctx = canvas.getContext('2d');

    // Встановлюємо розміри на весь екран
    canvas.width = window.innerWidth;
    canvas.height = window.innerHeight;

    // === ОГОЛОШЕННЯ ЗМІННИХ (Всередині IF) ===
    const logo = "FETCh";
    const letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Набір символів
    const fontSize = 100;

    // Центруємо логотип
    let logoX = canvas.width / 2 - (logo.length * fontSize) / 2;
    let logoY = canvas.height / 2;

    function draw() {
        // Додаткова перевірка
        if (!ctx) return;

        // 1. Затемнення фону для слідів (ефект шлейфу)
        ctx.fillStyle = 'rgba(0,0,0,0.2)';
        ctx.fillRect(0, 0, canvas.width, canvas.height);

        // 2. Глітч-логотип (Ваш оригінальний дизайн)
        ctx.fillStyle = '#0f0';
        ctx.font = fontSize + "px monospace"; // Додаємо шрифт, щоб текст відображався коректно

        for (let i = 0; i < logo.length; i++) {
            // Додаємо випадковий зміщений шум
            const glitchOffsetX = Math.random() * 6 - 3;
            const glitchOffsetY = Math.random() * 6 - 3;
            ctx.fillText(logo[i], logoX + i * fontSize + glitchOffsetX, logoY + glitchOffsetY);
        }

        // 3. Додаткові випадкові символи навколо (Ваш оригінальний дизайн)
        for (let i = 0; i < 10; i++) {
            const x = Math.random() * canvas.width;
            const y = Math.random() * canvas.height;
            const char = letters[Math.floor(Math.random() * letters.length)];
            ctx.fillText(char, x, y);
        }
    }

    // Оновлення кожні 50мс для плавного глітчу
    // (Запускаємо таймер ТІЛЬКИ тут, всередині перевірки if)
    setInterval(draw, 50);

    // Оновлення розмірів при зміні розміру вікна браузера
    window.addEventListener('resize', () => {
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;
        // Перераховуємо центр для логотипу
        logoX = canvas.width / 2 - (logo.length * fontSize) / 2;
        logoY = canvas.height / 2;
    });
}