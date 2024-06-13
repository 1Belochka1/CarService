import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export const passwordConfirmValidator: ValidatorFn = (
	group: AbstractControl
): ValidationErrors | null => {
	const password = group.get('password')
	const confirmPassword = group.get('confirmPassword')

	if (password == null || confirmPassword == null) return null

	if (password.value == '' || confirmPassword.value == '') return null

	if (password?.value === confirmPassword?.value) {
		password.setErrors(null)
		confirmPassword.setErrors(null)
		return null
	}

	if (password?.errors || confirmPassword?.errors) return null

	const error = {noConfirmPassword: true}

	password.setErrors(error)

	confirmPassword.setErrors(error)

	return error
}