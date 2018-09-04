import { NgModule, Injectable } from '@angular/core'
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";

import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from "@angular/http";  // legacy
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";   // use this

// components
import { AppHeader } from './common/appHeader';
import { AppFooter } from "./common/appFooter";

import { ProductList } from './products/productList';
import { ProductView } from './products/productView';
import { ProductEdit } from './products/productEdit';

// services
import { Product } from './products/entities';
import { ProductService } from './products/productService';
import { AppConfiguration } from './appConfiguration';

// providers
import { ErrorHandler } from './common/errorHandler';

@NgModule({
    declarations: [
        AppComponent,
        AppHeader,
        AppFooter,

        ProductList,
        ProductView,
        ProductEdit
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        HttpModule,
        HttpClientModule,
        AppRoutingModule
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: ErrorHandler,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
