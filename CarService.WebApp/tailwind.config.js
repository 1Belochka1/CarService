/** @type {import('tailwindcss').Config} */
const plugin = require('tailwindcss/plugin')

module.exports = {
	content: ['./src/**/*.{html,ts}'],
	theme: {
		screens: {
			'desktop': {max: '1280px'},

			'laptop': {max: '1024px'},

			'tablet': {max: '768px'},

			'mobile': {max: '375px'},
		},
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
			},
		},
	},
	plugins:
		[
			plugin(function ({addComponents, theme}) {
				addComponents({
					'.my-input': {},
					'.pagination-item': {
						minWidth: '24px',
						// background: 'rgba(var(--color-menu))',
						color: 'rgba(var(--color-on-menu))',
						textAlign: 'center',
						padding: theme('spacing.1'),
						// border: '1px solid
						// rgba(var(--color-primary))',
						cursor: 'pointer',
						userSelect: 'none'
					},
				})
			}),
		]
}
