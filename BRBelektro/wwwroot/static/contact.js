
  // email integration 

  console.log("script loaded");
  async function sendForm(event) {
    event.preventDefault(); 
    alert("sendForm runs");
    const data = {
      name: document.getElementById("name").value,
      email: document.getElementById("email").value,
      message: document.getElementById("message").value,
      phoneNumber: document.getElementById("phone").value
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