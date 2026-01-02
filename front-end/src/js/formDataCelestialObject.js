import axios from 'axios';
import Toastify from 'toastify-js';

// Function to display the modal with planet data
export const showModal = (modal, planetData, fetchCelestialObjects) => {
    // Construct HTML content for the modal with planet data
    const modalContent = `
        <h2 class="planet-name">${planetData.name}</h2>
        <label for="name">Satellites:</label>
        <label id="satelliteList"></label>
        
        <label for="name">Aliens:</label>
        <label id="alienList"></label>
        
        <label for="name">Name:</label>
        <input type="text" id="name" value="${planetData.name}" />

        <label for="size">Size:</label>
        <input type="number" id="size" value="${planetData.size}" />
        
        <label for="position">Position:</label>
        <input type="number" id="position" value="${planetData.position}" />
        
        <label for="mesh">Mesh:</label>
        <input type="text" id="mesh" value="${planetData.meshURL}" />
        
        <label for="obj">Obj:</label>
        <input type="text" id="obj" value="${planetData.objURL}" />
        
        <label for="urlImage">Image URL:</label>
        <input type="text" id="urlImage" value="${planetData.urlImage}" />
        
        <label for="positionDateTime">Position Date/Time:</label>
        <input type="text" id="positionDateTime" value="${planetData.positionDateTime}" />
        
        <label for="description">Description:</label>
        <textarea id="description">${planetData.description}</textarea>
        
        <label for="bestMomentsAndSpots">Best Moments and Spots:</label>
        <textarea id="bestMomentsAndSpots">${planetData.bestMomentsAndSpots}</textarea>
        
        <button class="delete-btn">Delete</button>
        <button class="save-btn">Save</button>
        <button class="close-btn">Close</button>
    `;

    // Fill the modal with the content
    modal.innerHTML = modalContent;
    modal.style.display = 'block';

    // Call functions to fetch satellites and aliens for the planet
    fetchSatellites(planetData.id);
    fetchAliens(planetData.id);

    // Add event listener for the save button
    const saveBtn = modal.querySelector('.save-btn');
    saveBtn.addEventListener('click', async () => {
        let name;

        try {
            // Collect form data
            name = document.getElementById('name').value;
            const size = parseFloat(document.getElementById('size').value);
            const position = parseFloat(document.getElementById('position').value);
            const meshURL = document.getElementById('mesh').value; // Note: meshURL is a string, not a number
            const objURL = document.getElementById('obj').value;   // Note: objURL is a string, not a number
            const urlImage = document.getElementById('urlImage').value;
            const positionDateTime = document.getElementById('positionDateTime').value;
            const description = document.getElementById('description').value;
            const bestMomentsAndSpots = document.getElementById('bestMomentsAndSpots').value;

            // Update planet data
            planetData.name = name;
            planetData.size = size;
            planetData.position = position;
            planetData.mesh = meshURL;
            planetData.obj = objURL;
            planetData.urlImage = urlImage;
            planetData.positionDateTime = positionDateTime;
            planetData.description = description;
            planetData.bestMomentsAndSpots = bestMomentsAndSpots;

            // Send a PUT request to update the planet
            console.log('Updated planet data:', planetData);
            const response = await axios.put(`https://localhost:7195/api/CelestialObject/${planetData.id}`, planetData, {
                headers: {
                    'Authorization': `Bearer ${authToken}`,
                    'Content-Type': 'application/json',
                },
            });
            // Show success message
            Toastify({
                text: `Celestial object "${name}" updated successfully`,
                duration: 2000,
                gravity: 'bottom',
                position: 'right',
                backgroundColor: 'green',
            }).showToast();

            // Close the modal
            modal.style.display = 'none';

            // Fetch updated celestial objects
            await fetchCelestialObjects();
        } catch (error) {
            // Handle errors during update
            console.error(`Error updating celestial object "${name}":`, error);
            Toastify({
                text: `Error updating celestial object "${name}": 
            Error: ${error.message || 'Unknown error'}`,
                duration: 2000,
                gravity: 'bottom',
                position: 'right',
                backgroundColor: 'red',
            }).showToast();

            // Close the modal in case of error
            modal.style.display = 'none';
        }
    });


    // Add event listener for the delete button
    const deleteBtn = modal.querySelector('.delete-btn');
    deleteBtn.addEventListener('click', async () => {
        try {
            // Send a DELETE request to remove the celestial object
            await axios.delete(`https://localhost:7195/api/CelestialObject/${planetData.id}`, {
                headers: {
                    'Authorization': `Bearer ${authToken}`,
                },
            });
            // Show success message
            Toastify({
                text: `Celestial object "${planetData.name}" deleted successfully`,
                duration: 2000,
                gravity: 'bottom',
                position: 'right',
                backgroundColor: 'green',
            }).showToast();

            // Close the modal
            modal.style.display = 'none';

            // Fetch updated celestial objects
            await fetchCelestialObjects();
        } catch (error) {
            // Handle errors during deletion
            console.error(`Error deleting celestial object "${planetData.name}":`, error);

            // Show error message
            Toastify({
                text: `Error deleting celestial object "${planetData.name}": ${error.message || 'Unknown error'}`,
                duration: 2000,
                gravity: 'bottom',
                position: 'right',
                backgroundColor: 'red',
            }).showToast();

            // Close the modal in case of error
            modal.style.display = 'none';
        }
    });

    // Add event listener for the close button
    const closeBtn = modal.querySelector('.close-btn');
    closeBtn.addEventListener('click', () => {
        // Close the modal
        modal.style.display = 'none';
    });
};
// Function to fetch satellites for the given celestial object
async function fetchSatellites(celestialObjectId) {
    try {
        if (celestialObjectId === undefined || isNaN(Number(celestialObjectId))) {
            console.error('Invalid celestialObjectId:', celestialObjectId);
            return;
        }

        // Fetch satellites for the celestial object using a GET request
        const response = await axios.get(`https://localhost:7195/api/CelestialObject/${celestialObjectId}/Satellites`, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json',
            },
        });

        const satellites = response.data;

        // Select the HTML element to display the list of satellites
        const satelliteList = document.getElementById('satelliteList');
        satelliteList.innerHTML = '';

        if (satellites.length > 0) {
            // Concatenate satellite names into a single string
            const satelliteNames = satellites.map(satellite => satellite.name).join(', ');

            // Create a list item element to display satellite names
            const listItem = document.createElement('li');
            listItem.textContent = satelliteNames;

            // Add style to remove automatic bullet point
            listItem.style.listStyleType = 'none';

            // Add the element to the satellite list
            satelliteList.appendChild(listItem);
        } else {
            // Display a message if there are no satellites
            const noSatellitesMessage = document.createElement('p');
            noSatellitesMessage.textContent = 'No satellites for this celestial object.';
            satelliteList.appendChild(noSatellitesMessage);
        }
    } catch (error) {
        // Handle errors during satellite fetch
        console.error('Error fetching satellites:', error);
    }
}

// Function to fetch aliens for the given celestial object
async function fetchAliens(celestialObjectId) {
    try {
        if (celestialObjectId === undefined || isNaN(Number(celestialObjectId))) {
            console.error('Invalid celestialObjectId:', celestialObjectId);
            return;
        }

        // Fetch aliens for the celestial object using a GET request
        const response = await axios.get(`https://localhost:7195/api/CelestialObject/${celestialObjectId}/Aliens`, {
            headers: {
                'Authorization': `Bearer ${authToken}`,
                'Content-Type': 'application/json',
            },
        });

        const aliens = response.data;

        // Select the HTML element to display the list of aliens
        const alienList = document.getElementById('alienList');
        alienList.innerHTML = '';

        if (aliens.length > 0) {
            // Concatenate alien names into a single string
            const alienNames = aliens.map(alien => alien.name).join(', ');

            // Create a list item element to display alien names
            const listItem = document.createElement('li');
            listItem.textContent = alienNames;

            // Add style to remove automatic bullet point
            listItem.style.listStyleType = 'none';

            // Add the element to the alien list
            alienList.appendChild(listItem);
        } else {
            // Display a message if there are no aliens
            const noAliensMessage = document.createElement('p');
            noAliensMessage.textContent = 'No aliens for this celestial object.';
            alienList.appendChild(noAliensMessage);
        }
    } catch (error) {
        // Handle errors during alien fetch
        console.error('Error fetching aliens:', error);
    }
}
