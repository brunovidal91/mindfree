
// --------------------- Modal ------------------------
function CloseButtons() {

	const closeButtons = document.querySelectorAll(".btn_close");
	closeButtons.forEach(button => {
		button.addEventListener('click', () => {
			const modalId = button.getAttribute('data-modal');
			const modal = document.getElementById(modalId);

			modal.close();
		});
	});
}


function OpenMyModal() {

    const openModalButtons = document.querySelectorAll(".openModal");

    openModalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalId = button.getAttribute('data-modal');
            const modal = document.getElementById(modalId);

            modal.showModal();
        });
    });
}