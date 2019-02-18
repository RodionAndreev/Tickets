import { Registration } from './../../models/registration';
import { Component, OnInit } from '@angular/core';
import { UsersService } from 'src/app/services/users/users.service';
// import { AlertService } from 'src/app/services/users/alert.service';
import { FormBuilder, FormGroup, FormControl, Validators} from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationForm: FormGroup;
  er: string;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private userSrv: UsersService,
    // private alertService: AlertService,
    private _location: Location) {
  }

  ngOnInit() {
    this.registrationForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(15), Validators.pattern('^[A-z0-9_-]*$')]],
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required, Validators.minLength(6), Validators.maxLength(12), Validators.pattern('^[A-z0-9_-]*$')],
      confirmpassword: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(12), Validators.pattern('^[A-z0-9_-]*$')]]
     } , {validator: this.checkPasswords });
  }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
  const pass = group.controls.password.value;
  const confirmPass = group.controls.confirmpassword.value;

  return pass === confirmPass ? null : { notSame: true };
}
  get f() { return this.registrationForm.controls; }

  registry() {
    this.submitted = true;

    if (this.registrationForm.invalid) {
      return;
    }

    this.loading = true;
    this.userSrv.sendUser(this.registrationForm.value)
        // .pipe(first())
        .subscribe(
          data => {
              // this.alertService.success('Registration successful', true);
              // this.router.navigate(['/login']);
              this._location.back();
          },
          error => {
               // this.alertService.error(error);
              this.loading = false;
          });
  }
    /*if (this.registrationForm.valid) {
    this.userSrv.sendUser(this.registrationForm.value).subscribe(err => { console.error(err); } );
    console.log('successfully registered');
    console.log(this.registrationForm.value);
    this._location.back();
    }
  }*/
}
