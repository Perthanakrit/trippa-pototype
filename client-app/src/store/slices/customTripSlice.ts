import agent from '../../api/agent';
import { RootState } from '../Store';
import { CustomTrip } from './../../types/tirp';
import { ActionReducerMapBuilder, createAsyncThunk, createSlice } from "@reduxjs/toolkit";

type CustomTripStates = {
    customTrips: CustomTrip[];
    customTrip: CustomTrip | null;
    loading: boolean;
}

const initialState:CustomTripStates  = {
    customTrips: [],
    customTrip: null,
    loading: false
}

const orderSlice = createSlice({
    name: "customTrip",
    initialState: initialState,
    reducers:{},
    extraReducers: (builder) => {
        setCustomTrips(builder, loadCustomTrips, loadCustomTrip);
    }
});

const loadCustomTrips = createAsyncThunk<CustomTrip[], void>('customTrip/loadCustomTrips', async () => {
    try {
        const customTrips:CustomTrip[] = await agent.CustomTrips.list();
        return customTrips;
    }
    catch (error) {
        console.log(error);
        return [];
    }
});

const loadCustomTrip = createAsyncThunk<CustomTrip | null, string>('customTrip/loadCustomTrip', async (id:string) => {
    try {
        const customTrip:CustomTrip = await agent.CustomTrips.details(id);
        return customTrip;
    }
    catch (error) {
        console.log(error);
        return null;
    }
});

// AsyncThunk<CustomTrip[] | undefined, void, Async>
const setCustomTrips = (builder: ActionReducerMapBuilder<CustomTripStates>, loadTrips:any, loadTrip:any) => {
    builder.addCase(loadTrips.fulfilled, (state, action) => {
        state.customTrips = action.payload;
        state.loading = false;
    });
    builder.addCase(loadTrips.pending, (state) => {
        state.loading = true;
    });
    builder.addCase(loadTrips.rejected, (state) => {
        state.customTrips = [];
        state.loading = false;
    });

    builder.addCase(loadTrip.fulfilled, (state, action) => {
        state.customTrip = action.payload;
        state.loading = false;
    }).addCase(loadTrip.pending, (state) => {
        state.loading = true;
    }).addCase(loadTrip.rejected, (state) => {
        state.customTrip = null;
        state.loading = false;
    });

}

export { loadCustomTrips, loadCustomTrip };
export const customTripSelector = (state:RootState) => state.customTripSlice;
export default orderSlice.reducer;