let slideIndex = 1;

// Slayd funksiyaları (DOMContentLoaded-dən kənarda olmalıdır ki, HTML-dən də çağırıla bilsin)
function changeSlide(n) {
    showSlides(slideIndex += n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("slide");
    if (slides.length === 0) return;

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }

    for (i = 0; i < slides.length; i++) {
        slides[i].classList.remove("active");
    }
    slides[slideIndex - 1].classList.add("active");
}

document.addEventListener('DOMContentLoaded', () => {

    // --- 1. Slayder Başlatma ---
    if (document.getElementsByClassName("slide").length > 0) {
        showSlides(slideIndex);
    }

    // --- 2. Hamburger Menu ---
    const hamburger = document.querySelector('.hamburger');
    const nav = document.querySelector('.nav');
    const header = document.querySelector('.header'); // Əgər orbital-shells ilə eynidirsə, aşağıdakı ilə birləşdirilə bilər

    if (hamburger && nav) {
        hamburger.addEventListener('click', () => {
            nav.classList.toggle('active');
            const icon = hamburger.querySelector('i');
            if (icon) {
                if (nav.classList.contains('active')) {
                    icon.classList.replace('fa-bars', 'fa-times');
                } else {
                    icon.classList.replace('fa-times', 'fa-bars');
                }
            }
        });
    }

    document.querySelectorAll('.nav__link').forEach(link => {
        link.addEventListener('click', () => {
            if (nav) nav.classList.remove('active');
            if (hamburger) {
                const icon = hamburger.querySelector('i');
                if (icon) icon.classList.replace('fa-times', 'fa-bars');
            }
        });
    });

    // --- 3. Scroll Efekti (Header) ---
    const orbitalHeader = document.querySelector(".orbital-shells");
    window.addEventListener('scroll', () => {
        if (orbitalHeader) {
            if (window.scrollY > 50) {
                orbitalHeader.classList.add('scrolled');
            } else {
                orbitalHeader.classList.remove('scrolled');
            }
        }
    });

    // --- 4. FAQ (Accordion) ---
    const faqHeaders = document.querySelectorAll('.faq-header');
    faqHeaders.forEach(fHeader => {
        fHeader.addEventListener('click', function () {
            const item = this.closest('.faq-item');
            const answer = item.querySelector('.faq-answer');
            const icon = item.querySelector('.faq-icon i');
            const isActive = item.classList.contains('active');

            document.querySelectorAll('.faq-item').forEach(otherItem => {
                if (otherItem !== item) {
                    otherItem.classList.remove('active');
                    const otherAnswer = otherItem.querySelector('.faq-answer');
                    if (otherAnswer) otherAnswer.style.maxHeight = null;
                    const otherIcon = otherItem.querySelector('.faq-icon i');
                    if (otherIcon) {
                        otherIcon.classList.replace('fa-minus', 'fa-plus');
                    }
                }
            });

            if (isActive) {
                item.classList.remove('active');
                if (answer) answer.style.maxHeight = null;
                if (icon) icon.classList.replace('fa-minus', 'fa-plus');
            } else {
                item.classList.add('active');
                if (answer) answer.style.maxHeight = answer.scrollHeight + "px";
                if (icon) icon.classList.replace('fa-plus', 'fa-minus');
            }
        });
    });

    // --- 5. Login Form Validasiyası ---
    const loginForm = document.querySelector('.login-form');
    const usernameInput = document.getElementById('UserNameOrEmail');
    const passwordInput = document.getElementById('Password');
    const errorBox = document.getElementById('error-box');

    if (loginForm && usernameInput && passwordInput) {
        loginForm.addEventListener('submit', function (e) {
            const userVal = usernameInput.value.trim();
            const passVal = passwordInput.value.trim();

            if (userVal === "" || passVal === "") {
                e.preventDefault();
                if (errorBox) {
                    errorBox.style.display = "block";
                    errorBox.innerHTML = "<strong>ERROR:</strong> Sahələr boş ola bilməz.";
                }
            } else {
                if (errorBox) errorBox.style.display = "none";
            }
        });
    }

    // --- 6. Lightbox ---
    const lightboxModal = document.getElementById('image-lightbox');
    const lightboxImg = document.getElementById('lightbox-img');
    const closeBtn = document.querySelector('.close-lightbox');
    const triggers = document.querySelectorAll('.lightbox-trigger');

    if (lightboxModal && lightboxImg) {
        triggers.forEach(trigger => {
            trigger.addEventListener('click', function (e) {
                e.preventDefault();
                const wrapper = this.closest('.related-image');
                if (wrapper) {
                    const img = wrapper.querySelector('img');
                    if (img) {
                        lightboxImg.src = img.src;
                        lightboxModal.style.display = "block";
                    }
                }
            });
        });

        if (closeBtn) {
            closeBtn.addEventListener('click', () => {
                lightboxModal.style.display = "none";
            });
        }

        lightboxModal.addEventListener('click', (e) => {
            if (e.target === lightboxModal) {
                lightboxModal.style.display = "none";
            }
        });
    }

    // --- 7. My Account Dropdown ---
    const dropdownContainer = document.querySelector('.prism-controller');
    const dropdownBtn = document.querySelector('.fusion-btn');

    if (dropdownBtn && dropdownContainer) {
        dropdownBtn.addEventListener('click', function (event) {
            event.preventDefault();
            dropdownContainer.classList.toggle('active');
        });

        document.addEventListener('click', function (event) {
            if (!dropdownContainer.contains(event.target)) {
                dropdownContainer.classList.remove('active');
            }
        });
    }
});