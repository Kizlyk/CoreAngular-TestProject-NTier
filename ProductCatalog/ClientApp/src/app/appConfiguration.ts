import { Injectable } from '@angular/core';

declare var $:any;
declare var toastr: any;
declare var location: any;

@Injectable({
    providedIn: 'root'
})
export class AppConfiguration {
    constructor() {
        this.setToastrOptions();
    }

    searchText = "";
    urls = {
        baseUrl: "./",
        products: "api/products",
        product: "api/product",
        exportExcel: "api/products/exportexcel",
        url: (name, parm1?, parm2?, parm3?) => {
            var url = this.urls.baseUrl + this.urls[name];
            if (parm1)
                url += "/" + parm1;
            if (parm2)
                url += "/" + parm2;
            if (parm3)
                url += "/" + parm3;

            return url;
        }
    };

    setToastrOptions() {
        toastr.options.closeButton = true;
        toastr.options.positionClass = "toast-top-center";
    }
}

