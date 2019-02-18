import { GetTicketByIdService } from './../../services/get-ticket-by-id.service';
import { Component, OnInit } from '@angular/core';
import { Ticket } from '../../models/ticket';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import * as jwt_decode from 'jwt-decode';
import { OAuthService } from 'angular-oauth2-oidc';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-ticket-detail',
  templateUrl: './ticket-detail.component.html',
  styleUrls: ['./ticket-detail.component.css']
})
export class TicketDetailComponent implements OnInit {
  id: string;
  tickets: Ticket;
  authenticated: boolean;
  addTicketForm: FormGroup;
  constructor(
    private ticketsSrv: GetTicketByIdService,
    private router: ActivatedRoute,
    private route: Router,
    private _location: Location,
    private oauthService: OAuthService
  ) {
    this.authenticated = this.ticketsSrv.isAuthenticated();
    router.params.subscribe(params => (this.id = params.id));
  }

  ngOnInit() {
    this.ticketsSrv
      .getTicketById(this.id)
      .subscribe(result => (this.tickets = result), err => console.error(err));
  }
  deleteTicketById(id) {
    this.ticketsSrv.delTicketById(id).subscribe(err => console.error(err));
  }
  goEditTicket(id) {
    {
      this.route.navigate(['tickets-ticket', id, 'edit']);
      this.route.navigateByUrl('tickets-ticket/' + id + '/edit');
    }
  }
  subscribe(id){
    this.addTicketForm = new FormGroup({
      'UserInfoId':   new FormControl(this.getUserId()),
      'FirstName' : new FormControl(this.getUserName())
      });
      console.log(this.addTicketForm.value)
      this.ticketsSrv.subsсribe(this.addTicketForm.value, id).subscribe(err => console.error(err));
  }
  getUserId(): string {
    var decodedToken = this.getDecodedAccessToken(
      this.oauthService.getAccessToken()
    );
    return decodedToken['id'];
  }
  isTicketCreator() {
    try {
      if (this.getUserId() == this.tickets.user.userInfoId) {
        return true;
      }
    } catch {
      return false;
    }
    return false;
  }
  respondedUsernull(ticket: Ticket) {
    try {
      if (ticket.respondedUsers != null &&ticket.respondedUsers.length!=0 ) { return true; }
    } catch { return false; }
    return false;
  }
  backClicked() {
    this._location.back();
  }
  getDecodedAccessToken(token: string): any {
    try {
      return jwt_decode(token);
    } catch (Error) {
      return null;
    }
  }
    getUserName(): string {
      var decodedToken = this.getDecodedAccessToken(this.oauthService.getAccessToken());
      if (decodedToken == null) return null;
      return decodedToken['name'];

      }

}
