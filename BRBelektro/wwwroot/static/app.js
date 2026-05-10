document.addEventListener("DOMContentLoaded", loadInquiries);

async function loadInquiries() {
    const token = sessionStorage.getItem('google_token');
    console.log("Loading inquiries");
    const response = await fetch('/api/inquiry', {
        headers: { 
            'Authorization': `Bearer ${token}`
        }
    });
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


async function deleteInquiry(id) {
    confirm("Are you sure you want to delete inquiry?");

    try {
        const token = sessionStorage.getItem('google_token');
        const response = await fetch(`/api/inquiry/${id}`, {
            method: "DELETE",
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            alert("Inquiry deleted");
            loadInquiries();
        } else {
            alert("Failed to delete inquiry.");
        }

    } catch (error) {
        console.error(error);
        alert("Something went wrong.");
    }

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
        beskrivelse: document.getElementById("description").value,
        dato: document.getElementById("date").value,
        status: document.getElementById("status").value,
        betaling
    };

    try {
        const token = sessionStorage.getItem('google_token');
        const response = await fetch("/api/inquiry", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
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

//editing inquiry//




async function fetchInquiry(id){
    const token = sessionStorage.getItem('google_token');
    console.log("Loading inquiry with id");
    const response = await fetch(`/api/inquiry/${id}`, {
        headers: { 
            'Authorization': `Bearer ${token}`
        }
    });
    const data = await response.json();
    return data;
}

async function editInquiry(id) {
    
    const inquiry = await fetchInquiry(id);
    document.getElementById("modalOverlay").style.display = "flex";

    document.getElementById("editId").value = inquiry.id;
    document.getElementById("editName").value = inquiry.name;
    document.getElementById("editEpost").value = inquiry.epost;
    document.getElementById("editTelefon").value = inquiry.telefon;
    document.getElementById("editBeskrivelse").value = inquiry.beskrivelse;
    document.getElementById("editDato").value = inquiry.dato;
    document.getElementById("editStatus").value = inquiry.status;
    document.getElementById("editBetaling").value = inquiry.betaling;
}

//saving edit into database//

async function saveEditForm(event) {
    event.preventDefault();
    const betaling = document.getElementById("editBetaling").value == "true" ?
        true : false;

    const id = document.getElementById("editId").value;

    const inquiry = {
        name: document.getElementById("editName").value,
        epost: document.getElementById("editEpost").value,
        telefon: document.getElementById("editTelefon").value,
        beskrivelse: document.getElementById("editBeskrivelse").value,
        dato: document.getElementById("editDato").value,
        status: document.getElementById("editStatus").value,
        betaling
    };

    try {
        const token = sessionStorage.getItem('google_token');
        const response = await fetch(`/api/inquiry/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(inquiry)
        });

        if (response.ok) {
            alert("Inquiry saved to database!");
            document.querySelector("form").reset();
            loadInquiries();
            closeEditForm();
        } else {
            alert("Failed to save inquiry.");
        }

    } catch (error) {
        console.error(error);
        alert("Something went wrong.");
    }
}

//closing the form //
function closeEditForm(e) {
    if (e) e.preventDefault();
    document.getElementById("modalOverlay").style.display = "none";
}

//close edit inquiry modal//

document.getElementById("modalOverlay").addEventListener("click", function(e) {
    if (e.target === this) {
        this.style.display = "none";
    }
});

