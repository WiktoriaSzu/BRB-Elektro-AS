
/* navbar injection */
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
  //await inject("footer", "/partials/footer.html");
})();


// opening and closing the burger menu on mobile or small screen //
document.addEventListener("click", (e) => {
  const burger = document.querySelector(".burger");
  const links = document.querySelector(".links");

  if (!burger || !links) return;

  // toggle the menu when clicking //
  if (e.target.closest(".burger")) {
    const open = links.classList.toggle("open");
    // for accessibility - screen readers know when menu is open //
    burger.setAttribute("aria-expanded", open);
  }

  // closes menu when a link is clicked //
  if (e.target.tagName === "A" && links.contains(e.target)) {
    links.classList.remove("open");
    // for accessibility - screen readers know when menu is closed //
    burger.setAttribute("aria-expanded", "false");
  }
});
