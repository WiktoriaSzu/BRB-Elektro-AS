document.addEventListener("DOMContentLoaded", loadInquiries);

async function loadInquiries() {
    console.log("Loading inquiries");
    const response = await fetch('/api/inquiry');
    const data = await response.json();

    const table = document.getElementById("InquiryTable");
    table.innerHTML = "";

    data.forEach(i => {
        const row = `
            <tr>
                <td>${i.id}</td>
                <td>${i.name}</td>
                <td>${i.epost}</td>
                <td>${i.telefon}</td>
                <td>${i.beskrivelse}</td>
                <td>${i.dato}</td>
                <td>${i.status}</td>
                <td>${i.betaling ? 'Yes' : 'No'}</td>
                <td>
                    <button class="btn-edit" onclick="editInquiry(${i.id})">Edit</button>
                    <button class="btn-delete" onclick="deleteInquiry(${i.id})">Delete</button>
                </td>
            </tr>
        `;
        table.innerHTML += row;
    });
}

//adding inquiry through form

async function sendInquiry(event) {
    event.preventDefault();

    const betaling = document.getElementById("payment").value == "true" ?
        true : false;

    const inquiry = {
        name: document.getElementById("name").value,
        epost: document.getElementById("email").value,
        telefon: document.getElementById("phone").value,
        description: document.getElementById("description").value,
        date: document.getElementById("date").value,
        status: document.getElementById("status").value,
        betaling
    };

    try {
        const response = await fetch("/api/inquiry", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(inquiry)
        });

        if (response.ok) {
            alert("Inquiry saved to database!");
            document.querySelector("form").reset();
            loadInquiries();
        } else {
            alert("Failed to save inquiry.");
        }

    } catch (error) {
        console.error(error);
        alert("Something went wrong.");
    }


}