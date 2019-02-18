import { Component, OnInit, OnDestroy } from '@angular/core';
import { Ticket } from '../../models/ticket';
import { GetallticketsService } from '../../services/getalltickets.service';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit, OnDestroy {
  pageid: number;
  tickets: Ticket[] = [];
  count: number;
  pages: number[] = [];
  subscription: Subscription;
  constructor(
    private ticketsSrv: GetallticketsService,
    private router: Router,
    private route: ActivatedRoute
    ) {
    }

  ngOnInit() {

  this.subscription = this.route.params.subscribe(
      (params: Params) => {
           this.pageid = params.id || 1;
           this.ticketsSrv.getAllTickets(this.pageid).subscribe(result => {
    this.tickets = result['tickets'];
    this.count = result['totalPages'];
    for (let i = 0; i < this.count + 1; i++) {
      this.pages[i] = i + 1;
    }
}, err => console.error(err));
});

}
ngOnDestroy(): void {
  this.subscription.unsubscribe();
}

  goToTicket(id) {
    this.router.navigate(['tickets-ticket', id]);
    this.router.navigateByUrl('tickets-ticket/' + id);
  }
  goToPage(id) {
    this.router.navigate(['tickets', id]);
    this.router.navigateByUrl('tickets/' + id);
  }
}
