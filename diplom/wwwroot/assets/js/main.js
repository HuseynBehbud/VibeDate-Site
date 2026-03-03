let slideIndex = 1;

document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementsByClassName("slide").length > 0) {
        showSlides(slideIndex);
    }
});

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

    const hamburger = document.querySelector('.hamburger');
    const nav = document.querySelector('.nav');
    const header = document.querySelector('.header');

    if (hamburger) {
        hamburger.addEventListener('click', () => {
            nav.classList.toggle('active');
            const icon = hamburger.querySelector('i');

            if (nav.classList.contains('active')) {
                icon.classList.remove('fa-bars');
                icon.classList.add('fa-times');
            } else {
                icon.classList.remove('fa-times');
                icon.classList.add('fa-bars');
            }
        });
    }

    document.querySelectorAll('.nav__link').forEach(link => {
        link.addEventListener('click', () => {
            nav.classList.remove('active');
            if (hamburger) {
                const icon = hamburger.querySelector('i');
                icon.classList.remove('fa-times');
                icon.classList.add('fa-bars');
            }
        });
    });

    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) {
            header.classList.add('scrolled');
        } else {
            header.classList.remove('scrolled');
        }
    });

    const headers = document.querySelectorAll('.faq-header');

    if (headers.length > 0) {
        headers.forEach(header => {
            header.addEventListener('click', function () {
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
                            otherIcon.classList.remove('fa-minus');
                            otherIcon.classList.add('fa-plus');
                        }
                    }
                });

                if (isActive) {
                    item.classList.remove('active');
                    answer.style.maxHeight = null;
                    icon.classList.remove('fa-minus');
                    icon.classList.add('fa-plus');
                } else {
                    item.classList.add('active');
                    answer.style.maxHeight = answer.scrollHeight + "px";
                    icon.classList.remove('fa-plus');
                    icon.classList.add('fa-minus');
                }
            });
        });
    }

    const loginForm = document.querySelector('.login-form');
    const usernameInput = document.getElementById('username');
    const passwordInput = document.getElementById('password');
    const errorBox = document.getElementById('error-box');

    if (loginForm) {
        loginForm.addEventListener('submit', function (e) {
            e.preventDefault(); 

            const userVal = usernameInput.value.trim();
            const passVal = passwordInput.value.trim();

            if (userVal === "" || passVal === "") {
                errorBox.style.display = "block";
                errorBox.innerHTML = "<strong>ERROR:</strong> Username must not be blank<br><strong>ERROR:</strong> Your username or password was incorrect";
            } else {
                errorBox.style.display = "none";
                alert("Login successful! (Redirecting...)");
            }
        });
    }

});
document.addEventListener('DOMContentLoaded', function () {
    const lightboxModal = document.getElementById('image-lightbox');
    const lightboxImg = document.getElementById('lightbox-img');
    const closeBtn = document.querySelector('.close-lightbox');
    const triggers = document.querySelectorAll('.lightbox-trigger');

    triggers.forEach(trigger => {
        trigger.addEventListener('click', function (e) {
            e.preventDefault(); 
            const wrapper = this.closest('.related-image');
            const img = wrapper.querySelector('img');
            lightboxImg.src = img.src;
            lightboxModal.style.display = "block";
        });
    });

    closeBtn.addEventListener('click', function () {
        lightboxModal.style.display = "none";
    });

    lightboxModal.addEventListener('click', function (e) {
        if (e.target === lightboxModal) {
            lightboxModal.style.display = "none";
        }
    });
});
    document.addEventListener('DOMContentLoaded', function () {
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
document.addEventListener("DOMContentLoaded", function () {
    const header = document.querySelector(".orbital-shells");
    window.addEventListener("scroll", function () {
        if (window.scrollY > 50) {
            header.classList.add("scrolled");
        } else {
            header.classList.remove("scrolled");
        }
    });
});
