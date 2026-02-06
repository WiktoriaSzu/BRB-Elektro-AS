
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
  await inject("footer", "/partials/footer.html");
})();

