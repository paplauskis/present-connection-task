export default function TrackingNumberSearchForm({
  trackingNumber,
  setTrackingNumber,
  onSubmit,
  onReset
}) {
  return (
    <form onSubmit={onSubmit}>
      <input
        type="text"
        placeholder="Search by Tracking Number"
        value={trackingNumber}
        onChange={(e) => setTrackingNumber(e.target.value)}
      />
      <button type="submit">Search</button>
      <button type="button" onClick={onReset}>
        Reset
      </button>
    </form>
  )
}