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