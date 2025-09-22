let score = 0;
let timeLeft = 10;
let gameActive = false;

const scoreEl = document.getElementById("score");
const timerEl = document.getElementById("timer");
const commitBtn = document.getElementById("commitBtn");

if (commitBtn) {
    commitBtn.addEventListener("click", () => {
        if (!gameActive) startGame();
        if (timeLeft > 0) {
            score++;
            scoreEl.textContent = score;
        }
    });
}

function startGame() {
    gameActive = true;
    score = 0;
    timeLeft = 10;
    scoreEl.textContent = score;
    timerEl.textContent = "Час: " + timeLeft;

    const countdown = setInterval(() => {
        timeLeft--;
        timerEl.textContent = "Час: " + timeLeft;
        if (timeLeft <= 0) {
            clearInterval(countdown);
            gameActive = false;
            timerEl.textContent = "Гру завершено!";
        }
    }, 1000);
}

// Scroll reveal
const animatedBlocks = document.querySelectorAll(".scroll-animate");
const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            entry.target.classList.add("visible");
        }
    });
}, { threshold: 0.2 });

animatedBlocks.forEach(block => observer.observe(block));

