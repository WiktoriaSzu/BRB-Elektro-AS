/* Loads the shared navbar and footer into every page */
async function inject(id, url) {
  const el = document.getElementById(id);
  if (!el) return;

  try {
    const res = await fetch(url);
    if (!res.ok) throw new Error(`${res.status} ${res.statusText} for ${url}`);
    el.innerHTML = await res.text();
  } catch (err) {
    console.error("Navbar/Footer inject failed:", err);
  }
}

(async () => {
  await inject("navbar", "/partials/navbar.html");
  await inject("footer", "/partials/footer.html");
})();

/* Burger menu and services dropdown on small screens */
document.addEventListener("click", (e) => {
  const burger = document.querySelector(".burger");
  const links = document.querySelector(".links");

  const servicesWrapper = document.querySelector(".dropdown-menu");
  const servicesToggle = document.querySelector(".menu-toggle");

  if (!links) return;

  // toggle the menu when clicking the burger
  if (e.target.closest(".burger")) {
    const open = links.classList.toggle("open");
    // lets screen readers know when the menu is open
    if (burger) {
      burger.setAttribute("aria-expanded", open);
    }
    return;
  }

  // toggle the services dropdown
  if (e.target.closest(".menu-toggle")) {
    if (window.matchMedia("(max-width:40rem)").matches) {
      e.preventDefault();
    }
    if (servicesWrapper && servicesToggle) {
      const open = servicesWrapper.classList.toggle("open");
      servicesToggle.setAttribute("aria-expanded", String(open));
    }
    return;
  }

  // close the menu when a link is clicked
  if (e.target.tagName === "A" && links.contains(e.target)) {
    links.classList.remove("open");
    if (burger) {
      burger.setAttribute("aria-expanded", "false");
    }
    servicesWrapper?.classList.remove("open");
  }
});

/* Image and project sliders */
document.querySelectorAll(".slider").forEach(slider => {
  const slides = slider.querySelectorAll(".slide");
  const leftBtn = slider.querySelector(".arrow.left");
  const rightBtn = slider.querySelector(".arrow.right");
  if (slides.length === 0 || !leftBtn || !rightBtn) return;

  let current = 0;

  function showSlide(index) {
    slides.forEach(slide => slide.classList.remove("active"));
    slides[index].classList.add("active");
  }

  rightBtn.addEventListener("click", () => {
    current = (current + 1) % slides.length;
    showSlide(current);
  });

  leftBtn.addEventListener("click", () => {
    current = (current - 1 + slides.length) % slides.length;
    showSlide(current);
  });
});

/* Certificate popup on the competences page */
const modal = document.getElementById("certificateModal");
const modalImg = document.getElementById("modalImage");
const closeBtn = document.querySelector(".close-modal");

if (modal && modalImg) {
  document.querySelectorAll(".cert-trigger").forEach(img => {
    img.addEventListener("click", () => {
      modal.style.display = "flex";
      modalImg.src = img.dataset.cert;
    });
  });

  closeBtn?.addEventListener("click", () => {
    modal.style.display = "none";
  });

  modal.addEventListener("click", (e) => {
    if (e.target === modal) {
      modal.style.display = "none";
    }
  });
}
