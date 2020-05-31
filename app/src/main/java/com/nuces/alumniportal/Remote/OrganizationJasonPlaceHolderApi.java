package com.nuces.alumniportal.Remote;

import com.nuces.alumniportal.DatabaseClass.Organization;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface OrganizationJasonPlaceHolderApi {
    @GET("organizations")
    Call<List<Organization>> getOrganizations();

}
