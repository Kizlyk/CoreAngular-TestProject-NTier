import {Injectable} from '@angular/core';
import {Product} from './entities';
import {AppConfiguration} from "../appConfiguration";
import {Observable} from "rxjs";
import {map, catchError} from "rxjs/operators";
import { HttpClient } from "@angular/common/http";
import { HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private httpClient: HttpClient,
        private config: AppConfiguration) {
    }

    newProduct(): Product {
        return new Product();
    }

    getProducts(searchText: string): Observable<any> {
        var productsUrl = this.config.urls.products;
        if (searchText) {
            productsUrl += "?$filter=contains(name, '" + searchText + "')";
        }
        return this.httpClient.get<Product[]>(productsUrl);
    }

    getProduct(id): Observable<Product> {
        return this.httpClient.get<Product>(this.config.urls.url("product", id));
    }

    deleteProduct(product: Product): Observable<any> {
        return this.httpClient.delete(this.config.urls.url("product", product.id));
    }

    saveProduct(product): Observable<any> {
        if (product.id) {
            return this.httpClient.put(this.config.urls.url("product"), product);
        }
        else {
            return this.httpClient.post(this.config.urls.url("product"), product);
        }
    }

    exportProducts(searchText: string) {
        var exportUrl = this.config.urls.exportExcel;
        if (searchText) {
            exportUrl += "?$filter=contains(name, '" + searchText + "')";
        }
        return this.httpClient.get(exportUrl, {
            responseType: "blob",
            headers: new HttpHeaders().append("Content-Type", "application/json")
        });
    }
}
