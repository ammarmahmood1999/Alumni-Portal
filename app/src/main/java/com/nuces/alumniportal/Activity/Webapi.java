package com.nuces.alumniportal.Activity;

import android.app.AlertDialog;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.widget.Button;
import android.widget.TextView;
import com.nuces.alumniportal.DatabaseClass.Organization;
import com.nuces.alumniportal.R;
import com.nuces.alumniportal.Remote.OrganizationJasonPlaceHolderApi;

import java.util.List;

import dmax.dialog.SpotsDialog;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class Webapi extends AppCompatActivity {


    OrganizationJasonPlaceHolderApi organizationJasonPlaceHolderApi;
    TextView textView;
    Button button;
    AlertDialog dialog;
    StringBuilder Org_name = new StringBuilder();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_web_api);

        button = findViewById(R.id.webapibutton);
        textView = findViewById(R.id.webapitext);
        dialog = new SpotsDialog(Webapi.this);

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl("http://192.168.1.108:45457//api/")
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        OrganizationJasonPlaceHolderApi organizationJasonPlaceHolderApi = retrofit.create(OrganizationJasonPlaceHolderApi.class);
        Call<List<Organization>> call = organizationJasonPlaceHolderApi.getOrganizations();
        call.enqueue(new Callback<List<Organization>>() {
            @Override
            public void onResponse(Call<List<Organization>> call, Response<List<Organization>> response) {
                if(!response.isSuccessful()) {
                    textView.setText("Code: " + response.code());
                    return;
                }
                List<Organization> organizations = response.body();

                for(Organization organization : organizations){
                    String content ="";
                    content+="City: "+ organization.getCity()+"\n";
                    content+="Country: "+ organization.getCountry()+"\n";
                    content+="Email: "+ organization.getEmail()+"\n";
                    content+="Location: "+ organization.getLocation()+"\n";
                    content+="Name: "+ organization.getName()+"\n";
                    Org_name.append(organization.getName());
                    content+="Id: "+ organization.getId()+"\n\n\n";

                    textView.append(content);


                }
                Log.d("ammar", Org_name.toString());

            }


            @Override
            public void onFailure(Call<List<Organization>> call, Throwable t) {
                textView.setText((t.getMessage()));
            }
        });
    }
}