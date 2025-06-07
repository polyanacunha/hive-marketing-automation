import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ProductDTO } from '../models/product.dto';

@Component({
  selector: 'app-products-list',
  standalone: true,
  imports: [],
  templateUrl: './products-list.component.html',
})
export class ProductsListComponent implements OnInit {
  products: ProductDTO[] = [];

  constructor(private svc: ProductService) {}

  ngOnInit() {
    this.svc.getAll().subscribe(list => this.products = list);
  }
}
