// Sends the contact form to the backend, which saves it and emails the company.
async function sendForm(event) {
  event.preventDefault();
  const form = event.target;
  const button = form.querySelector("button[type=submit]");

  const data = {
    name: document.getElementById("name").value,
    email: document.getElementById("email").value,
    message: document.getElementById("message").value,
    phoneNumber: document.getElementById("phone").value
  };

  button.disabled = true;
  try {
    const response = await fetch("/api/contact", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(data)
    });

    if (response.ok) {
      alert("Forespørsel sendt!");
      form.reset();
    } else if (response.status === 429) {
      alert("Du har sendt mange forespørsler. Prøv igjen om litt.");
    } else {
      alert("Noe gikk galt. Sjekk feltene og prøv igjen.");
    }
  } catch (error) {
    console.error(error);
    alert("Noe har gått galt...");
  } finally {
    button.disabled = false;
  }
}
