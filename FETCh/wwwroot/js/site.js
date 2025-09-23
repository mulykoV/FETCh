(() => {
    let score = 0;
    let timeLeft = 10;
    let gameActive = false;
    let countdown = null;

    const scoreEl = document.getElementById("score");
    const timerEl = document.getElementById("timer");
    const commitBtn = document.getElementById("commitBtn");
    const resultBox = document.getElementById("resultBox");
    const typedText = document.getElementById("typedText");
    const consoleLine = document.getElementById("consoleLine");

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
                timerEl.textContent = ""; // прибираємо таймер
                showCertificate();
            }
        }, 1000);
    }

    function showCertificate() {
        const registerUrl = document.getElementById("registerUrl").dataset.url;
        // Ховаємо кнопку git commit
        if (consoleLine) consoleLine.style.display = "none";
        if (resultBox) {
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
    }


    if (commitBtn) {
        commitBtn.addEventListener("click", () => {
            if (!gameActive) startGame();
            if (gameActive && timeLeft > 0) {
                score++;
                scoreEl.textContent = score;
            }
        });
    }

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

    window.addEventListener("DOMContentLoaded", () => {
        resultBox.classList.add("hidden");
        resultBox.innerHTML = "";
        consoleLine.style.display = "flex";
        commitBtn.style.display = "inline-flex";
        timerEl.textContent = "Час: 10";
        typeGitCommit();
    });
})();

// --- Scroll reveal
const animatedBlocks = document.querySelectorAll(".scroll-animate");
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
