import { AbstractControl, ValidationErrors } from "@angular/forms";

export class CustomValidators {
  static passwordsMatching(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    const passwordConfirm = control.get('passwordConfirm')?.value;

    if ((password === passwordConfirm) && (password !== null && passwordConfirm !== null)) {
      return null;
    } else {
      return { passwordsNotMatching: true };
    }
  }

  static strongPassword(control: AbstractControl): ValidationErrors | null {
    const password = control.get('password')?.value;
    var regex = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{8,}$/;


    if ( password !== null  && regex.test(password)){
      return null;
    } else {
      return {weaksPassword: true };
    }
  }

}
