import {Product} from './entities';
import {Component, OnInit, Input, OnDestroy} from '@angular/core';
import {style, animate, state, transition, trigger} from '@angular/animations';
import {ProductService} from "./productService";
import {ActivatedRoute, Router} from "@angular/router";
import {AppConfiguration} from "../appConfiguration";
import { slideIn, slideInLeft } from "../common/animations";

declare var $: any;
declare var toastr: any;
declare var window: any;

@Component({
    selector: 'product-view',
    templateUrl: './productView.html',
    animations: [slideIn]
})
export class ProductView implements OnInit {

    @Input() product: Product = new Product();
    aniFrame = 'in';

    constructor(private route: ActivatedRoute,
        private router: Router,
        private config: AppConfiguration,
        private productService: ProductService) {
    }

    ngOnInit() {
        if (!this.product.name) {
            var id = this.route.snapshot.params["id"];
            if (id < 1)
                return;

            this.productService.getProduct(id)
                .subscribe(result => {
                    this.product = result;
                });
        }
    }

    ngOnDestroy() {
        this.aniFrame = 'out';
        console.log("ngDestroy")
    }

    deleteProduct(product) {
        if (confirm("Confirm deleting this product?")) {
            this.productService.deleteProduct(product)
                .subscribe(result => {
                    var msg = "Product " + this.product.name + " has been deleted."
                    toastr.success(msg);
                    setTimeout(() => this.router.navigate(["/products"]), 1500);
                });
        }
    }
}
