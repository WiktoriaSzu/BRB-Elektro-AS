

console.log("TEsting!")
console.log("Is anything here?")

/* navbar injection */
async function inject(id, url) {
  const el = document.getElementById(id);
  if (!el) {
    console.error("No element with id:", id)
    return;
  }

  try {
    const res = await fetch(url);
    if (!res.ok) throw new Error(`${res.status} ${res.statusText} for ${url}`);
    el.innerHTML = await res.text();
    console.log("setting innerHTML to: ", el.innerHTML)
  } catch (err) {
    console.error("Navbar/Footer inject failed:", err);
  }
}

(async () => {
  await inject("navbar", "../partials/navbar.html");
  await inject("footer", "../partials/footer.html");
  console.log("injecting navbar!");
})();

// opening and closing the burger menu on mobile or small screen //
document.addEventListener("click", (e) => {
  const burger = document.querySelector(".burger");
  const links = document.querySelector(".links");

  const servicesWrapper = document.querySelector(".dropdown-menu");
  const servicesToggle = document.querySelector(".menu-toggle");

  if (!links) return;

  // toggle the menu when clicking //
  if (e.target.closest(".burger")) {
    const open = links.classList.toggle("open");
    // for accessibility - screen readers know when menu is open //
    if (burger) {
      burger.setAttribute("aria-expanded", open);
    }
    return;
  }

  /* toggling services */
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
  // closes menu when a link is clicked //
  if (e.target.tagName === "A" && links.contains(e.target)) {
    links.classList.remove("open");
    if (burger) {
      burger.setAttribute("aria-expanded", "false");
    }

    servicesWrapper?.classList.remove("open");
  }
});

document.querySelectorAll(".slider").forEach(slider => {
  const slides = slider.querySelectorAll(".slide");
  const leftBtn = slider.querySelector(".arrow.left");
  const rightBtn = slider.querySelector(".arrow.right");

  let current = 0;

  function showSlide(index) {
    slides.forEach(slide => slide.classList.remove("active"));
    slides[index].classList.add("active");
  }

  if(leftBtn && rightBtn && slides.length > 0) {
    rightBtn.addEventListener("click", () => {
      current = (current + 1) % slides.length;
      showSlide(current);
    });

  leftBtn.addEventListener("click", () => {
    current = (current - 1 + slides.length) % slides.length;
    showSlide(current);
    });
  }
});


/* kompetanser modals */

const modal = document.getElementById("certificateModal");
const modalImg = document.getElementById("modalImage");
const closeBtn = document.querySelector(".close-modal");

console.log("Trying to add click on image")
document.querySelectorAll(".cert-trigger").forEach(img => {
  console.log("Adding click listener to image")
  img.addEventListener("click", () => {
    if(modal && modalImg) {
    console.log("I was clicked!")
    modal.style.display = "flex";
    modalImg.src = img.dataset.cert;
    }
  });
});

if(closeBtn && modal) {
  closeBtn.onclick = () => {
    modal.style.display = "none";
  };

  modal.onclick = (e) => {
    if (e.target === modal) {
      modal.style.display = "none";
    }
  };
}

fetch('/api/hello')
  .then(res => res.json())
  .then(data => {
    console.log(data.message);
  })

  // email integration 

  console.log("script loaded");
  async function sendForm(event) {
    event.preventDefault(); 
    alert("sendForm runs");
    const data = {
      name: document.getElementById("name").value,
      email: document.getElementById("email").value,
      message: document.getElementById("message").value
    };
    try {
      const response = await fetch("/api/contact", {
      method: "POST", 
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });
    
    if (response.ok) {
      alert("Førespørsel sendt!");
    } else {
      alert("Prøv igjen.")
    }
  } catch (error) {
    console.error(error);
    alert("Noe har gått galt...");
  }
}