package com.nuces.alumniportal.Remote;

import com.nuces.alumniportal.DatabaseClass.Alumni;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.GET;

public interface AlumniJasonPlaceHolderApi {
    @GET("alumni")
    Call<List<Alumni>> getAlumni();
}
