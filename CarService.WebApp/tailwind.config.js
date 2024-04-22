/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin');

module.exports = {
	content: ['./src/**/*.{html,ts}'],
	theme: {
		fontFamily: {
			montserrat: ['Montserrat', 'sans-serif'],
		},
		extend: {
			spacing: {
				65: '16.875rem',
			},
			colors: {
				'background': 'rgba(var(--color-background),<alpha-value>)',
				'background-element': 'rgba(var(--color-background-element),<alpha-value>)',
				// title: 'rgba(var(--color-text-title),<alpha-value>)',
				// primary: 'rgba(var(--color-text-primary),<alpha-value>)',
				// secondary: 'rgba(var(--color-text-secondary),<alpha-value>)',
			}
		},
	},

};
