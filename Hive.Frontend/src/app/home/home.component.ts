import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarHomeComponent } from '../navbar-home/navbar-home.component';
import { ContentHomeComponent } from '../content-home/content-home.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, NavbarHomeComponent, ContentHomeComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
