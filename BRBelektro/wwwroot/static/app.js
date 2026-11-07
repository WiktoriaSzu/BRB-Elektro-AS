document.addEventListener("DOMContentLoaded", loadInquiries);

// Calls the admin API with the Google token and sends the user back to the
// login page if the token is missing, expired or not allowed.
async function callApi(url, options = {}) {
    const token = sessionStorage.getItem('google_token');
    const response = await fetch(url, {
        ...options,
        headers: {
            ...options.headers,
            'Authorization': `Bearer ${token}`
        }
    });

    if (response.status === 401) {
        window.location.href = '/login.html';
    }
    if (response.status === 403) {
        sessionStorage.removeItem('google_token');
        window.location.href = '/login.html?error=unauthorized';
    }

    return response;
}

// Builds a table cell with textContent so customer input is never parsed as HTML.
function cell(value) {
    const td = document.createElement("td");
    td.textContent = value ?? "";
    return td;
}

function actionButton(label, className, onClick) {
    const button = document.createElement("button");
    button.textContent = label;
    button.className = className;
    button.addEventListener("click", onClick);
    return button;
}

async function loadInquiries() {
    const response = await callApi('/api/inquiry');
    if (!response.ok) return;

    const data = await response.json();
    const table = document.getElementById("InquiryTable");
    table.replaceChildren();

    data.forEach(i => {
        const row = document.createElement("tr");
        row.append(
            cell(i.id),
            cell(i.name),
            cell(i.epost),
            cell(i.telefon),
            cell(i.beskrivelse),
            cell(i.dato),
            cell(i.status),
            cell(i.betaling ? "Betalt" : "Ikke betalt")
        );

        const actions = document.createElement("td");
        actions.append(
            actionButton("Rediger", "btn-edit", () => editInquiry(i.id)),
            actionButton("Slett", "btn-delete", () => deleteInquiry(i.id))
        );
        row.append(actions);
        table.append(row);
    });
}

async function deleteInquiry(id) {
    if (!confirm("Er du sikker på at du vil slette henvendelsen?")) return;

    try {
        const response = await callApi(`/api/inquiry/${id}`, {
            method: "DELETE"
        });

        if (response.ok) {
            loadInquiries();
        } else {
            alert("Kunne ikke slette henvendelsen.");
        }
    } catch (error) {
        console.error(error);
        alert("Noe gikk galt.");
    }
}

// Adding an inquiry from the form
async function sendInquiry(event) {
    event.preventDefault();
    const form = event.target;

    const inquiry = {
        name: document.getElementById("name").value,
        epost: document.getElementById("email").value,
        telefon: document.getElementById("phone").value || null,
        beskrivelse: document.getElementById("description").value,
        dato: document.getElementById("date").value || null,
        status: document.getElementById("status").value,
        betaling: document.getElementById("payment").value === "true"
    };

    try {
        const response = await callApi("/api/inquiry", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(inquiry)
        });

        if (response.ok) {
            form.reset();
            loadInquiries();
        } else {
            alert("Kunne ikke lagre henvendelsen.");
        }
    } catch (error) {
        console.error(error);
        alert("Noe gikk galt.");
    }
}

// Editing an inquiry
async function fetchInquiry(id) {
    const response = await callApi(`/api/inquiry/${id}`);
    return await response.json();
}

async function editInquiry(id) {
    const inquiry = await fetchInquiry(id);
    document.getElementById("modalOverlay").style.display = "flex";

    document.getElementById("editId").value = inquiry.id;
    document.getElementById("editName").value = inquiry.name ?? "";
    document.getElementById("editEpost").value = inquiry.epost ?? "";
    document.getElementById("editTelefon").value = inquiry.telefon ?? "";
    document.getElementById("editBeskrivelse").value = inquiry.beskrivelse ?? "";
    document.getElementById("editDato").value = inquiry.dato ?? "";
    document.getElementById("editStatus").value = inquiry.status ?? "Ny";
    document.getElementById("editBetaling").value = String(inquiry.betaling === true);
}

async function saveEditForm(event) {
    event.preventDefault();
    const id = document.getElementById("editId").value;

    const inquiry = {
        name: document.getElementById("editName").value,
        epost: document.getElementById("editEpost").value,
        telefon: document.getElementById("editTelefon").value || null,
        beskrivelse: document.getElementById("editBeskrivelse").value,
        dato: document.getElementById("editDato").value || null,
        status: document.getElementById("editStatus").value,
        betaling: document.getElementById("editBetaling").value === "true"
    };

    try {
        const response = await callApi(`/api/inquiry/${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(inquiry)
        });

        if (response.ok) {
            closeEditForm();
            loadInquiries();
        } else {
            alert("Kunne ikke lagre endringene.");
        }
    } catch (error) {
        console.error(error);
        alert("Noe gikk galt.");
    }
}

function closeEditForm(e) {
    if (e) e.preventDefault();
    document.getElementById("modalOverlay").style.display = "none";
}

// Close the edit modal when clicking outside it
document.getElementById("modalOverlay").addEventListener("click", function (e) {
    if (e.target === this) {
        closeEditForm();
    }
});
