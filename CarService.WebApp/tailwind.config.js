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
				'surface': 'rgba(var(--color-surface),<alpha-value>)',
				'menu': 'rgba(var(--color-menu),<alpha-value>)',
				'on-menu': 'rgba(var(--color-on-menu),<alpha-value>)',
				'primary': 'rgba(var(--color-primary),<alpha-value>)',
				'secondary': 'rgba(var(--color-secondary),<alpha-value>)',
				// title: 'rgba(var(--color-text-title),<alpha-value>)',
				// title: 'rgba(var(--color-text-title),<alpha-value>)',
				// primary: 'rgba(var(--color-text-primary),<alpha-value>)',
			}
		},
	},

};
