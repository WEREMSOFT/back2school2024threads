const { performance } = require('perf_hooks');

function speedTest() {
    const LARGE_NUMBER = 20000;

    let elements = new Array(LARGE_NUMBER);
    for (let i = 0; i < LARGE_NUMBER; i++) {
        elements[i] = new Array(LARGE_NUMBER);
    }

    const start = performance.now();

    
	for (let j = 0; j < LARGE_NUMBER; j++) {
	for (let i = 0; i < LARGE_NUMBER; i++) {
            elements[i][j] = i * j;
        }
    }

    const end = performance.now();

    const microseconds = (end - start) * 1000; // Convert to microseconds
    console.log(`Tiempo transcurrido: ${microseconds.toFixed(0)} microsegundos`);
}

speedTest();