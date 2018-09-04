import {Component, OnInit } from '@angular/core';
import { ProductService } from './productService';
import { Product } from './entities';

import {AppConfiguration} from "../appConfiguration";
import {Router} from "@angular/router";
import { slideIn, slideInLeft } from "../common/animations";
import { window } from 'rxjs/operators';
import { saveAs } from "file-saver/FileSaver";
import { config } from 'rxjs';

declare var $:any;
declare var toastr:any;

@Component({
    selector: 'product-list',
    templateUrl: './productList.html',
    animations: [slideIn]
})
export class ProductList implements OnInit {

    constructor(private router: Router,
        private productService: ProductService,
        private config: AppConfiguration) {
    }

    productList: Product[] = [];
    searchText: string = "";
    busy: boolean = true;

    ngOnInit() {
        this.getProducts();
        this.searchText = this.config.searchText;
    }

    getProducts() {
        this.busy = true;
        this.productList = [];
        this.productService.getProducts(this.config.searchText)
            .subscribe(products => {
                this.productList = products;
                this.busy = false;
            }, error => {
                this.busy = false;
            });
    }

    productClick(product: Product) {
        this.router.navigate(['/product', product.id]);
    }

    exportProducts() {
        this.productService.exportProducts(this.config.searchText).subscribe(file => {
            saveAs(file, 'Products.xlsx', { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' })
        });
    }

    searchProducts(searchText: string) {
        this.config.searchText = searchText;
        this.getProducts();
    }
}
