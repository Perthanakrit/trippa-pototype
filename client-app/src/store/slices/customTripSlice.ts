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
        setCustomTrips(builder, loadCustomTrips);
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
// AsyncThunk<CustomTrip[] | undefined, void, Async>
const setCustomTrips = (builder: ActionReducerMapBuilder<CustomTripStates>, loadAction:any) => {
    builder.addCase(loadAction.fulfilled, (state, action) => {
        state.customTrips = action.payload;
        state.loading = false;
    });

    builder.addCase(loadAction.pending, (state) => {
        state.loading = true;
    });

    builder.addCase(loadAction.rejected, (state) => {
        state.customTrips = [];
        state.loading = false;
    });
}

export { loadCustomTrips };
export const customTripSelector = (state:RootState) => state.customTripSlice;
export default orderSlice.reducer;