import {Component, OnInit, ElementRef} from '@angular/core';
import {Product} from "./entities";
import {ProductService} from "./productService";
import {Router, ActivatedRoute} from "@angular/router";
import {AppConfiguration} from "../appConfiguration";
import { slideInLeft, slideIn } from "../common/animations";

declare var $: any;
declare var toastr: any;
declare var window: any;

@Component({
    selector: 'product-edit',
    templateUrl: 'productEdit.html',
    animations: [slideIn]
})
export class ProductEdit implements OnInit {
    constructor(private route: ActivatedRoute,
        private router: Router,
        private productService: ProductService,
        private config: AppConfiguration) {
    }

    product: Product = new Product();
    loaded = false;
    aniFrame = 'in';

    ngOnInit() {
        var id = this.route.snapshot.params["id"];
        if (id < 1) {
            this.loaded = true;
            this.product = this.productService.newProduct();
            return;
        }
        this.productService.getProduct(id)
            .subscribe(result => {
                this.product = result;
                this.loaded = true;
            });
    }

    saveProduct(product: Product) {
        if (!this.validateProduct(product)) {
            return;
        }
        return this.productService.saveProduct(product)
            .subscribe((id: number) => {
                if (id) {
                    this.product.id = id;
                }
                var msg = this.product.name + " has been saved."
                toastr.success(msg);
                setTimeout(() => this.router.navigate(["/product", product.id]), 1500);
            });
    };

    validateProduct(product: Product): boolean {
        if (product.price < 0) {
            toastr.error("Product price cannot be negative");
            return false;
        }
        else if (product.price > 999) {
            return confirm("Price is too high, do you confirm?");
        }
        return true;
    }
}
