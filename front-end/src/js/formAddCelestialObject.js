import axios from 'axios';
import Toastify from 'toastify-js';

// Constante pour l'URL de base de l'API
const API_BASE_URL = 'https://localhost:7195/api/CelestialObject';

// Fonction principale pour afficher le formulaire d'ajout
const showAddForm = (modal, fetchCelestialObjects) => {
    // Récupération du contenu du formulaire
    const addFormContent = getAddFormContent();
    // Affichage du formulaire dans la modal
    displayModal(modal, addFormContent);

    // Ajout d'un écouteur d'événement sur la modal pour gérer les clics sur les boutons
    modal.addEventListener('click', async (event) => {
        if (event.target.classList.contains('save-btn')) {
            // Gestion du clic sur le bouton de sauvegarde
            await handleSaveButtonClick(modal, fetchCelestialObjects);
        } else if (event.target.classList.contains('close-btn')) {
            // Gestion du clic sur le bouton de fermeture
            handleCloseButtonClick(modal);
        }
    });
};

// Fonction pour obtenir le contenu HTML du formulaire
const getAddFormContent = () => {
    return `
  <label for="name">Name:</label>
        <input type="text" id="name" />

        <label for="size">Size:</label>
        <input type="number" id="size" />

        <label for="position">Position:</label>
        <input type="number" id="position" />

        <label for="mesh">Mesh:</label>
        <input type="text" id="mesh" />

        <label for="obj">Obj:</label>
        <input type="text" id="obj" />

        <label for="urlImage">Image URL:</label>
        <input type="text" id="urlImage" />

        <label for="positionDateTime">Position Date/Time:</label>
        <input type="text" id="positionDateTime" />

        <label for="description">Description:</label>
        <textarea id="description"></textarea>

        <label for="bestMomentsAndSpots">Best Moments and Spots:</label>
        <textarea id="bestMomentsAndSpots"></textarea>

        <button class="save-btn">Save</button>
        <button class="close-btn" onclick="closeModal()">Close</button>    `;
};

// Fonction pour afficher la modal avec le contenu spécifié
const displayModal = (modal, content) => {
    modal.innerHTML = content;
    modal.style.display = 'block';
};

// Fonction pour gérer le clic sur le bouton de sauvegarde
const handleSaveButtonClick = async (modal, fetchCelestialObjects) => {
    try {
        // Collecte des données du formulaire
        const formData = collectFormData();
        // Envoi d'une requête POST pour ajouter l'objet céleste
        const response = await axios.post(API_BASE_URL, formData, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json',
            },
        });        // Affichage du message de succès
        showSuccessMessage();
        // Fermeture de la modal
        closeModal(modal);
        // Rafraîchissement des objets célestes
        await fetchCelestialObjects();
    } catch (error) {
        // Gestion des erreurs
        handleErrorMessage(error);
        // Fermeture de la modal en cas d'erreur
        closeModal(modal);
    }
};

// Fonction pour collecter les données du formulaire
const collectFormData = () => {
    // Collect form data here and return as an object
    const name = document.getElementById('name').value;
    const size = parseFloat(document.getElementById('size').value);
    const position = parseFloat(document.getElementById('position').value);
    const mesh = document.getElementById('mesh').value;
    const obj = document.getElementById('obj').value;
    const urlImage = document.getElementById('urlImage').value;
    const positionDateTime = document.getElementById('positionDateTime').value;
    const description = document.getElementById('description').value;
    const bestMomentsAndSpots = document.getElementById('bestMomentsAndSpots').value;

    return {
        name,
        size,
        position,
        mesh,
        obj,
        urlImage,
        positionDateTime,
        description,
        bestMomentsAndSpots,
    };
};

// Fonction pour afficher le message de succès
const showSuccessMessage = () => {
    Toastify({
        text: 'Celestial object added successfully',
        duration: 2000,
        gravity: 'bottom',
        position: 'right',
        backgroundColor: 'green',
    }).showToast();
};

// Fonction pour gérer les messages d'erreur
const handleErrorMessage = (error) => {
    console.error('Error adding celestial object:', error);
    Toastify({
        text: `Error adding celestial object: ${error.message || 'Unknown error'}`,
        duration: 2000,
        gravity: 'bottom',
        position: 'right',
        backgroundColor: 'red',
    }).showToast();
};

// Fonction pour fermer la modal
const closeModal = (modal) => {
    modal.style.display = 'none';
};

// Fonction pour gérer le clic sur le bouton de fermeture
const handleCloseButtonClick = (modal) => {
    closeModal(modal);
};

// Exportation des fonctions nécessaires
export { showAddForm, closeModal };
