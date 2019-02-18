import { OAuthService } from 'angular-oauth2-oidc';
import { Address } from './../../models/address';
import { AddTicketService } from './../../services/add-ticket.service';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormBuilder,
  ReactiveFormsModule
} from '@angular/forms';
import { Ticket } from 'src/app/models/ticket';
import { Location } from '@angular/common';
import { GetTicketByIdService } from 'src/app/services/get-ticket-by-id.service';
import { ActivatedRoute } from '@angular/router';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-ticket-edit',
  templateUrl: './ticket-edit.component.html',
  styleUrls: ['./ticket-edit.component.css']
})
export class TicketEditComponent implements OnInit {
  addTicketForm: FormGroup;
  authenticated: boolean;
  id: string;
  ticket: Ticket;

  constructor(
    private ticketaddSrv: AddTicketService,
    private _location: Location,
    private oauthService: OAuthService,
    private ticketsSrv: GetTicketByIdService,
    private router: ActivatedRoute,
  ) {
    this.authenticated = this.ticketaddSrv.isAuthenticated();
    router.params.subscribe(params => this.id = params.id);
    this.addTicketForm = new FormGroup({
      'name' : new FormControl(),
      'free' : new FormControl(),
      'locationEvent' : new FormGroup({
        'country' : new FormControl(),
        'city' : new FormControl(),
        'street' : new FormControl(),
        'house' : new FormControl(),
        'flat' : new FormControl(),
      }),
      'sellerAdress' : new FormGroup({
        'country' : new FormControl(),
        'city' : new FormControl(),
        'street' : new FormControl(),
        'house' : new FormControl(),
        'flat' : new FormControl(),
      }),
      'eventLink' : new FormControl(),
      'additionalData' : new FormControl(),
      'typeEvent' : new FormControl(),
      'sellerPhone' : new FormControl(),
      'timeActual' : new FormControl(),
      'user' : new FormGroup({
        'userInfoId':   new FormControl(this.getUserId()),
        'firstName' : new FormControl(this.getUserName())
      }),
    });
  }

  ngOnInit() {
    this.ticketsSrv.getTicketById(this.id).subscribe(result => (this.ticket = result), err => console.error(err));
    console.log(this.id);
  }

  onSubmit() {
    console.log(this.addTicketForm.value);

    this.ticketaddSrv.updateTicket(this.addTicketForm.value, this.id).subscribe(err => {return console.error(err);});
  }
  getUserId(): string {
    var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
    if (decodedToken == null) return null;
    return decodedToken['id'];

    }
    getUserName(): string {
      var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
      if (decodedToken == null) return null;
      return decodedToken['name'];

      }


    getDecodedAccessToken(token: string): any {
      try{
          return jwt_decode(token);
      }
      catch(Error){
          return null;
      }
    }
}
