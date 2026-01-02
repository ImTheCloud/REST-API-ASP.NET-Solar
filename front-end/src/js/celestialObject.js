import * as THREE from 'three';
import { OrbitControls } from 'three/examples/jsm/controls/OrbitControls.js';
import axios from 'axios';
import Toastify from 'toastify-js';
import starsTexture from '../img/stars.jpg';
import sunTexture from '../img/sun.jpg';
import { showAddForm } from "./formAddCelestialObject";
import { showModal } from "./formDataCelestialObject";

// Renderer setup
const renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);
document.body.appendChild(renderer.domElement);

// Scene setup
const scene = new THREE.Scene();

// Camera setup
const camera = new THREE.PerspectiveCamera(
    45,
    window.innerWidth / window.innerHeight,
    0.1,
    1000
);

// Orbit controls for camera movement
const orbit = new OrbitControls(camera, renderer.domElement);
camera.position.set(-90, 140, 140);
orbit.update();

// Ambient light for scene illumination
const ambientLight = new THREE.AmbientLight(0x333333);
scene.add(ambientLight);

// Background setup with stars texture
const cubeTextureLoader = new THREE.CubeTextureLoader();
scene.background = cubeTextureLoader.load([
    starsTexture,
    starsTexture,
    starsTexture,
    starsTexture,
    starsTexture,
    starsTexture
]);

// Texture loader for sun texture
const textureLoader = new THREE.TextureLoader();

// Sun setup
const sunGeo = new THREE.SphereGeometry(16, 30, 30);
const sunMat = new THREE.MeshBasicMaterial({
    map: textureLoader.load(sunTexture)
});
const sun = new THREE.Mesh(sunGeo, sunMat);
scene.add(sun);

// Point light for additional lighting
const pointLight = new THREE.PointLight(0xFFFFFF, 2, 300);
scene.add(pointLight);

// Array to store planet data
const planets = [];

// Raycaster and mouse vector for click detection
const raycaster = new THREE.Raycaster();
const mouse = new THREE.Vector2();

// Event listener for click to show planet details
window.addEventListener('click', onMouseClick, false);
function onMouseClick(event) {
    event.preventDefault();
    mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
    mouse.y = - (event.clientY / window.innerHeight) * 2 + 1;
    raycaster.setFromCamera(mouse, camera);
    const intersects = raycaster.intersectObjects(planets.map(planet => planet.mesh));

    if (intersects.length > 0) {
        const clickedPlanet = intersects[0].object;
        const planetData = planets.find(planet => planet.mesh === clickedPlanet);
        showModal(modal, planetData, fetchCelestialObjects);
    }
}

// Button for adding a celestial object
const addButton = document.createElement('button');
addButton.textContent = 'Add celestial object';
addButton.style.position = 'fixed';
addButton.style.bottom = '10px';
addButton.style.right = '10px';
document.body.appendChild(addButton);

// Event listener for adding a celestial object
addButton.addEventListener('click', () => {
    showAddForm(modal, fetchCelestialObjects);
});

// Modal setup
const modal = document.createElement('div');
modal.classList.add('modal');
document.body.appendChild(modal);

// Function to create a planet mesh and object from data
const createPlanetFromData = (data) => {
    const geo = new THREE.SphereGeometry(data.size, 30, 30);
    const mat = new THREE.MeshStandardMaterial({
        map: textureLoader.load(data.urlImage),
    });
    const mesh = new THREE.Mesh(geo, mat);
    const obj = new THREE.Object3D();
    obj.add(mesh);
    scene.add(obj);
    mesh.position.x = data.position;

    // Set the rotateSpeed based on the fetched data
    const rotateSpeed = data.mesh;
    const meshURL = data.mesh;
    const objURL = data.obj;

    planets.push({
        mesh,
        obj,
        rotateSpeed,
        meshURL,
        objURL,
        name: data.name,
        size: data.size,
        position: data.position,
        urlImage: data.urlImage,
        positionDateTime: data.positionDateTime,
        description: data.description,
        bestMomentsAndSpots: data.bestMomentsAndSpots,
        id: data.id,
        timer: 0,
        timeoutId: null,
    });

    return { mesh, obj };
};

// Function to fetch celestial objects from the server
const fetchCelestialObjects = async (token) => {
    try {
        planets.forEach((planet) => {
            scene.remove(planet.obj);
        });
        planets.length = 0;

        const response = await axios.get('https://localhost:7195/api/CelestialObject', {
            headers: {
                'Authorization': `Bearer ${authToken}`, // Utilisez le token stocké
                'Content-Type': 'application/json',
            },
        });

        const celestialObjectsData = response.data;
        celestialObjectsData.forEach((data, index) => {
            const rotateSpeed = 0.01 * (index + 1); // Adjust the rotation speed based on the planet's position
            createPlanetFromData(data, rotateSpeed);
        });
    } catch (error) {
        console.error('Error fetching celestial objects:', error);
    }
};


// Initial fetch of celestial objects
fetchCelestialObjects();

// Map to store timeouts for planet arrival messages
const planetTimeouts = new Map();

// Function to check and display planet arrival messages
function checkPlanetArrival(planetData) {
    const timeToWaitInSeconds = planetData.position / 4;

    // Check if a timeout has already been set for the planet
    if (!planetTimeouts.has(planetData.name)) {
        const timeoutId = setTimeout(() => {
            Toastify({
                text: `Planet "${planetData.name}" has completed one orbit around the sun`,
                duration: 2000,
                gravity: 'bottom',
                position: 'left',
                backgroundColor: 'blue',
            }).showToast();

            // Remove the timeout ID from the map after the toast is sent
            planetTimeouts.delete(planetData.name);
        }, timeToWaitInSeconds * 1000);

        // Store the timeout ID in the map for reference
        planetTimeouts.set(planetData.name, timeoutId);
    }
}

// Animation loop function
function animate() {
    // Self-rotation for the sun
    sun.rotateY(0.004);

    // Rotate the fetched planets
    planets.forEach((planet) => {
        // Self-rotation
        planet.mesh.rotateY(planet.rotateSpeed);

        // Around-sun-rotation
        planet.obj.rotateY(planet.rotateSpeed);

        checkPlanetArrival(planet);
    });

    // Render the scene
    renderer.render(scene, camera);
}

// Set the animation loop
renderer.setAnimationLoop(animate);

// Event listener for window resize
window.addEventListener('resize', function () {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
});

// CSS styles for the modal
const modalStyle = `
  .modal {
        display: none;
        position: fixed;
        top: 15%;
        left: 50%;
        transform: translateX(-50%);
        max-height: 70%;
        max-width: 400px;
        overflow-y: auto;
        padding: 15px;
        background-color: #fff;
        border: 1px solid #ccc;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        z-index: 1000;
        width: 80%;
        box-sizing: border-box;
    }

    label {
        display: block;
        margin-top: 8px;
        font-weight: bold;
        font-size: 14px;
    }

    input,
    textarea {
        width: 100%;
        padding: 5px;
        margin-top: 5px;
        box-sizing: border-box;
        font-size: 12px;
    }

    .planet-name {
        text-align: center;
        font-size: 18px;
        margin-bottom: 10px;
    }

    .delete-btn,
    .save-btn,
    .close-btn {
        margin-top: 8px;
        padding: 8px 12px;
        cursor: pointer;
        display: inline-block;
        margin: 0 6px;
        border: none;
        border-radius: 4px;
        font-weight: bold;
    }

    .delete-btn {
        background-color: #ff3333;
        color: #fff;
    }

    .delete-btn:hover {
        background-color: #cc0000;
    }

    .save-btn {
        background-color: #4caf50;
        color: #fff;
    }

    .save-btn:hover {
        background-color: #45a049;
    }

    .close-btn {
        background-color: #001a33;
        color: #fff;
    }

    .close-btn:hover {
        background-color: #0056b3;
    }
`;

// Create a style element and append it to the head
const styleElement = document.createElement('style');
styleElement.innerHTML = modalStyle;
document.head.appendChild(styleElement);
